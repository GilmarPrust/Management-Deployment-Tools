namespace API.Control.Endpoints
{
    public static class ManufacturerEndPoints
    {
        public static RouteGroupBuilder MapManufacturerEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/manufacturers")
                .WithTags("Manufacturers")
                .WithName("ManufacturerEndpoints")
                .WithSummary("Endpoints for managing manufacturers")
                .WithDescription("Provides endpoints to create, read, update, and delete manufacturers.");

            // GET all
            group.MapGet("/", async (IManufacturerService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            });

            // GET by Id
            group.MapGet("/{id:guid}", async (Guid id, IManufacturerService service) =>
            {
                var result = await service.GetByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async (ManufacturerCreateDTO dto, IManufacturerService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/manufacturers/{created.Id}", created);
            });

            // DELETE
            group.MapDelete("/{id:guid}", async (Guid id, IManufacturerService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
