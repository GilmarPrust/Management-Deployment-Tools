namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de imagens de sistema operacional.
    /// </summary>
    public static class ImageEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de Image no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapImageEndpoints(this RouteGroupBuilder group)
        {
            // GET all Images
            group.MapGet("/", async (IImageService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllImages")
            .WithSummary("Obtém todas as imagens de sistema operacional.")
            .WithDescription("Retorna uma lista de todas as imagens de sistema operacional cadastradas.");

            // GET Image by Id
            group.MapGet("/{id:guid}", async (Guid id, IImageService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetImageById")
            .WithSummary("Obtém uma imagem de sistema operacional pelo Id.")
            .WithDescription("Retorna os dados de uma imagem de sistema operacional específica pelo seu identificador.");

            // POST Image
            group.MapPost("/", async (ImageCreateDTO dto, IImageService service, IValidator<ImageCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/images/{created.Id}", created);
            })
            .WithName("CreateImage")
            .WithSummary("Cria uma nova imagem de sistema operacional.")
            .WithDescription("Adiciona uma nova imagem de sistema operacional ao sistema.");

            // PUT Image
            group.MapPut("/{id:guid}", async (Guid id, ImageUpdateDTO dto, IImageService service, IValidator<ImageUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateImage")
            .WithSummary("Atualiza uma imagem de sistema operacional existente.")
            .WithDescription("Atualiza os dados de uma imagem de sistema operacional pelo seu identificador.");

            // DELETE Image
            group.MapDelete("/{id:guid}", async (Guid id, IImageService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteImage")
            .WithSummary("Remove uma imagem de sistema operacional.")
            .WithDescription("Remove uma imagem de sistema operacional do sistema pelo seu identificador.");

            return group;
        }
    }
}
