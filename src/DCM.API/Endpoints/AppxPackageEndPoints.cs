namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de AppxPackage.
    /// </summary>
    public static class AppxPackageEndPoints
    {
        /// <summary>
        /// Mapeia os endpoints de AppxPackage no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapAppxPackageEndPoints(this RouteGroupBuilder group)
        {
            // GET all
            group.MapGet("/", async (IAppxPackageService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllAppxPackages")
            .WithSummary("Lista todos os Appx Packages")
            .WithDescription("Retorna todos os pacotes Appx cadastrados.");

            // GET by Id
            group.MapGet("/{id:guid}", async (Guid id, IAppxPackageService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetAppxPackageById")
            .WithSummary("Busca um Appx Package pelo Id")
            .WithDescription("Retorna o pacote Appx correspondente ao Id informado.");

            // POST
            group.MapPost("/", async (AppxPackageCreateDTO dto, IAppxPackageService service, IValidator<AppxPackageCreateDTO> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/appxpackages/{created.Id}", created);
            })
            .WithName("CreateAppxPackage")
            .WithSummary("Cria um novo Appx Package")
            .WithDescription("Cria um novo pacote Appx com os dados informados.");

            // PUT
            group.MapPut("/{id:guid}", async (Guid id, AppxPackageUpdateDTO dto, IAppxPackageService service, IValidator<AppxPackageUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateAppxPackage")
            .WithSummary("Atualiza um Appx Package")
            .WithDescription("Atualiza os dados de um pacote Appx existente.");

            // DELETE
            group.MapDelete("/{id:guid}", async (Guid id, IAppxPackageService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteAppxPackage")
            .WithSummary("Remove um Appx Package")
            .WithDescription("Remove o pacote Appx correspondente ao Id informado.");

            return group;
        }
    }
}