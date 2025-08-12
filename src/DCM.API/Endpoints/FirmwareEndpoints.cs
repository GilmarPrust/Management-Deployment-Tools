namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de firmware.
    /// </summary>
    public static class FirmwareEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de Firmware no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapFirmwareEndpoints(this RouteGroupBuilder group)
        {
            // GET all Firmwares
            group.MapGet("/", async (IFirmwareService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllFirmwares")
            .WithSummary("Obtém todos os firmwares.")
            .WithDescription("Retorna uma lista de todos os firmwares cadastrados.");

            // GET Firmware by Id
            group.MapGet("/{id:guid}", async (Guid id, IFirmwareService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetFirmwareById")
            .WithSummary("Obtém um firmware pelo Id.")
            .WithDescription("Retorna os dados de um firmware específico pelo seu identificador.");

            // POST Firmware
            group.MapPost("/", async (FirmwareCreateDTO dto, IFirmwareService service, IValidator<FirmwareCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/firmwares/{created.Id}", created);
            })
            .WithName("CreateFirmware")
            .WithSummary("Cria um novo firmware.")
            .WithDescription("Adiciona um novo firmware ao sistema.");

            // PUT Firmware
            group.MapPut("/{id:guid}", async (Guid id, FirmwareUpdateDTO dto, IFirmwareService service, IValidator<FirmwareUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateFirmware")
            .WithSummary("Atualiza um firmware existente.")
            .WithDescription("Atualiza os dados de um firmware pelo seu identificador.");

            // DELETE Firmware
            group.MapDelete("/{id:guid}", async (Guid id, IFirmwareService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteFirmware")
            .WithSummary("Remove um firmware.")
            .WithDescription("Remove um firmware do sistema pelo seu identificador.");

            return group;
        }
    }
}
