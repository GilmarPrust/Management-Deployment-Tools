namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de dispositivos.
    /// </summary>
    public static class DeviceEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de Device no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapDeviceEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/devices")
                .WithTags("Devices")
                .WithName("DeviceEndpoints")
                .WithSummary("Endpoints for managing devices")
                .WithDescription("Provides endpoints to create, read, update, and delete devices.");

            // GET all Devices
            group.MapGet("/", async (IDeviceService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllDevices")
            .WithSummary("Obtém todos os dispositivos.")
            .WithDescription("Retorna uma lista de todos os dispositivos cadastrados.");

            // GET Device by Id
            group.MapGet("/{id:guid}", async (Guid id, IDeviceService service) =>
            {
                var result = await service.GetByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName("GetDeviceById")
            .WithSummary("Obtém um dispositivo pelo Id.")
            .WithDescription("Retorna os dados de um dispositivo específico pelo seu identificador.");

            // POST Device
            group.MapPost("/", async (DeviceCreateDTO dto, IDeviceService service, IValidator<DeviceCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/devices/{created.Id}", created);
            })
            .WithName("CreateDevice")
            .WithSummary("Cria um novo dispositivo.")
            .WithDescription("Adiciona um novo dispositivo ao sistema.");

            // PUT Device
            group.MapPut("/{id:guid}", async (Guid id, DeviceUpdateDTO dto, IDeviceService service, IValidator<DeviceUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateDevice")
            .WithSummary("Atualiza um dispositivo existente.")
            .WithDescription("Atualiza os dados de um dispositivo pelo seu identificador.");

            // DELETE Device
            group.MapDelete("/{id:guid}", async (Guid id, IDeviceService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteDevice")
            .WithSummary("Remove um dispositivo.")
            .WithDescription("Remove um dispositivo do sistema pelo seu identificador.");

            return group;
        }
    }
}
