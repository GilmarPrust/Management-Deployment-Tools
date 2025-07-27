namespace API.Control.Endpoints
{
    public static class ProfileTaskEndPoint
    {
        public static RouteGroupBuilder MapProfileTaskEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/profiletasks")
                .WithTags("Profile Tasks")
                .WithName("ProfileTaskEndpoints")
                .WithSummary("Endpoints for managing Profile Tasks")
                .WithDescription("Provides endpoints to create, read, update, and delete profile tasks.");

            // GET all
            group.MapGet("/", async ([FromServices] IProfileTaskService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IProfileTaskService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ([FromServices] IProfileTaskService service, ProfileTaskCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/profiletasks/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IProfileTaskService service, Guid id, ProfileTaskUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var updated = await service.UpdateAsync(id, dto);
                return updated ? Results.Ok() : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IProfileTaskService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
