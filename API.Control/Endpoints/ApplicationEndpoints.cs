namespace API.Control.Endpoints
{
    public static class ApplicationEndpoints
    {
        public static RouteGroupBuilder MapApplicationEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/applications")
                .WithTags("Applications")
                .WithName("ApplicationEndpoints")
                .WithSummary("Endpoints for managing applications")
                .WithDescription("Provides endpoints to create, read, update, and delete applications.");

            // GET all
            group.MapGet("/", async ([FromServices] IApplicationService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IApplicationService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ([FromServices] IApplicationService service, ApplicationCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/applications/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IApplicationService service, Guid id, ApplicationUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IApplicationService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
