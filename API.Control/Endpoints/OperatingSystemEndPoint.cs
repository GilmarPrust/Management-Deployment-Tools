namespace API.Control.Endpoints
{
    /// <summary>
    /// Extensão para mapear endpoints de sistemas operacionais.
    /// </summary>
    public static class OperatingSystemEndPoint
    {
        public static RouteGroupBuilder MapOperatingSystemEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/operatingsystems")
                .WithTags("Operating Systems")
                .WithName("OperatingSystemEndpoints")
                .WithSummary("Endpoints for managing operating systems")
                .WithDescription("Provides endpoints to create, read, update, and delete operating systems.");

            // GET all
            group.MapGet("/", async (IOperatingSystemService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            });

            // GET by Id
            group.MapGet("/{id:guid}", async (Guid id, IOperatingSystemService service) =>
            {
                var result = await service.GetByIdAsync(id);
                return result is not null
                    ? Results.Ok(result)
                    : Results.NotFound($"Sistema operacional não encontrado: {id}");
            });

            // POST
            group.MapPost("/", async (OperatingSystemCreateDTO dto, IOperatingSystemService service) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/operatingsystems/{created.Id}", created);
            });

            // DELETE
            group.MapDelete("/{id:guid}", async (Guid id, IOperatingSystemService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted
                    ? Results.NoContent()
                    : Results.NotFound($"Sistema operacional não encontrado: {id}");
            });

            return group;
        }
    }
}
