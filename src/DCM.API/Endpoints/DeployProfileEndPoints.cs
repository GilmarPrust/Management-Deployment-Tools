namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de DeployProfile.
    /// </summary>
    public static class DeployProfileEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de DeployProfile no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapDeployProfileEndpoints(this RouteGroupBuilder group)
        {
            // GET all
            group.MapGet("/", async (IDeployProfileService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllDeployProfiles")
            .WithSummary("Lista todos os perfis de implantação")
            .WithDescription("Retorna todos os perfis de implantação cadastrados.");

            // GET by Id
            group.MapGet("/{id:guid}", async (Guid id, IDeployProfileService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetDeployProfileById")
            .WithSummary("Busca um perfil de implantação pelo Id")
            .WithDescription("Retorna o perfil de implantação correspondente ao Id informado.");

            // POST
            group.MapPost("/", async (DeployProfileCreateDTO dto, IDeployProfileService service, IValidator<DeployProfileCreateDTO> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/deployprofiles/{created.Id}", created);
            })
            .WithName("CreateDeployProfile")
            .WithSummary("Cria um novo perfil de implantação")
            .WithDescription("Cria um novo perfil de implantação com os dados informados.");

            // PUT (atualização)
            group.MapPut("/{id:guid}", async (Guid id, DeployProfileUpdateDTO dto, IDeployProfileService service, IValidator<DeployProfileUpdateDTO> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateDeployProfile")
            .WithSummary("Atualiza um perfil de implantação")
            .WithDescription("Atualiza os dados de um perfil de implantação existente.");

            // DELETE
            group.MapDelete("/{id:guid}", async (Guid id, IDeployProfileService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteDeployProfile")
            .WithSummary("Remove um perfil de implantação")
            .WithDescription("Remove o perfil de implantação correspondente ao Id informado.");

            // GET - Applications by DeployProfileId
            group.MapGet("/{id:guid}/applications", async (Guid id, IDeployProfileService service) =>
            {
                var applications = await service.GetByIdAsync(id);
                return applications is not null ? Results.Ok(applications) : Results.NotFound();
            })
            .WithName("GetApplicationsByDeployProfileId");

            // GET - Devices by DeployProfileId
            group.MapGet("/{id:guid}/devices", async (Guid id, IDeployProfileService service) =>
            {
                var devices = await service.GetByIdAsync(id);
                return devices is not null ? Results.Ok(devices) : Results.NotFound();
            })
            .WithName("GetDevicesByDeployProfileId");

            // PUT - Update devices
            group.MapPut("/{id:guid}/devices", async (Guid id, AppxPackageDevicesUpdateDTO dto, IDeployProfileService service, IValidator<AppxPackageDevicesUpdateDTO> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var result = await service.UpdateAsync(id, dto.Deploy);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateDevicesByDeployProfileId");

            // PUT - Add device
            group.MapPut("/{id:guid}/device", async (Guid id, AppxPackageDevicesAddDTO dto, IDeployProfileService service, IValidator<AppxPackageDevicesAddDTO> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var result = await service.AddDeviceAsync(id, dto.DeviceId);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("AddDeviceToDeployProfile");

            // DELETE - Remove device
            group.MapDelete("/{id:guid}/device/{deviceId:guid}", async (Guid id, Guid deviceId, IDeployProfileService service) =>
            {
                var result = await service.RemoveDeviceAsync(id, deviceId);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("RemoveDeviceFromDeployProfile");

            return group;
        }
    }
}
