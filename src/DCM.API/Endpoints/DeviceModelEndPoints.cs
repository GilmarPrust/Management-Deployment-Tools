namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de modelos de dispositivo.
    /// </summary>
    public static class DeviceModelEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de DeviceModel no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapDeviceModelsEndpoints(this RouteGroupBuilder group)
        {
            // GET all DeviceModels
            group.MapGet("/", async (IDeviceModelService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllDeviceModels")
            .WithSummary("Obtém todos os modelos de dispositivo.")
            .WithDescription("Retorna uma lista de todos os modelos de dispositivo cadastrados.");

            // GET DeviceModel by Id
            group.MapGet("/{id:guid}", async (Guid id, IDeviceModelService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetDeviceModelById")
            .WithSummary("Obtém um modelo de dispositivo pelo Id.")
            .WithDescription("Retorna os dados de um modelo de dispositivo específico pelo seu identificador.");

            // POST DeviceModel
            group.MapPost("/", async (DeviceModelCreateDTO dto, IDeviceModelService service, IValidator<DeviceModelCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/devicemodels/{created.Id}", created);
            })
            .WithName("CreateDeviceModel")
            .WithSummary("Cria um novo modelo de dispositivo.")
            .WithDescription("Adiciona um novo modelo de dispositivo ao sistema.");

            // PUT DeviceModel
            group.MapPut("/{id:guid}", async (Guid id, DeviceModelUpdateDTO dto, IDeviceModelService service, IValidator<DeviceModelUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateDeviceModel")
            .WithSummary("Atualiza um modelo de dispositivo existente.")
            .WithDescription("Atualiza os dados de um modelo de dispositivo pelo seu identificador.");

            // DELETE DeviceModel
            group.MapDelete("/{id:guid}", async (Guid id, IDeviceModelService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteDeviceModel")
            .WithSummary("Remove um modelo de dispositivo.")
            .WithDescription("Remove um modelo de dispositivo do sistema pelo seu identificador.");

            return group;
        }
    }
}
