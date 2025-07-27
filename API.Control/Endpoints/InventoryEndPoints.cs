namespace API.Control.Endpoints
{
    public static class InventoryEndpoints
    {
        public static RouteGroupBuilder MapInventoryEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/inventories")
                .WithTags("Inventory")
                .WithName("InventoryEndpoints")
                .WithSummary("Endpoints for managing inventory items")
                .WithDescription("Provides endpoints to create, read, update, and delete inventory items.");

            // GET all
            group.MapGet("/", async ([FromServices] IInventoryService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IInventoryService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ( [FromServices] IInventoryService service, InventoryCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/inventories/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IInventoryService service, Guid id, InventoryUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IInventoryService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
