namespace API.Control.Endpoints
{
    public static class FirmwareEndpoints
    {
        public static RouteGroupBuilder MapFirmwareEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/firmwares")
                .WithTags("Firmware")
                .WithName("FirmwareEndpoints")
                .WithSummary("Endpoints for managing firmware")
                .WithDescription("Provides endpoints to create, read, update, and delete firmware.");

            // GET all
            group.MapGet("/", async ([FromServices] IFirmwareService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IFirmwareService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ([FromServices] IFirmwareService service, FirmwareCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/firmwares/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IFirmwareService service, Guid id, FirmwareUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IFirmwareService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
