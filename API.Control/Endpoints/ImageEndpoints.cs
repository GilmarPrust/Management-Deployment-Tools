namespace API.Control.Endpoints
{
    public static class ImageEndpoints
    {
        public static RouteGroupBuilder MapImageEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/images")
                .WithTags("Images")
                .WithName("ImageEndpoints")
                .WithSummary("Endpoints for managing images")
                .WithDescription("Provides endpoints to create, read, update, and delete images.");

            // GET all
            group.MapGet("/", async ([FromServices] IImageService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IImageService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ([FromServices] IImageService service, ImageCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/images/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IImageService service, Guid id, ImageUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IImageService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
