namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de fabricantes.
    /// </summary>
    public static class ManufacturerEndPoints
    {
        /// <summary>
        /// Mapeia os endpoints de Manufacturer no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapManufacturerEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/manufacturers")
                .WithTags("Manufacturers")
                .WithName("ManufacturerEndpoints")
                .WithSummary("Endpoints for managing manufacturers")
                .WithDescription("Provides endpoints to create, read, update, and delete manufacturers.");

            // GET all Manufacturers
            group.MapGet("/", async (IManufacturerService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllManufacturers")
            .WithSummary("Obtém todos os fabricantes.")
            .WithDescription("Retorna uma lista de todos os fabricantes cadastrados.");

            // GET Manufacturer by Id
            group.MapGet("/{id:guid}", async (Guid id, IManufacturerService service) =>
            {
                var result = await service.GetByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName("GetManufacturerById")
            .WithSummary("Obtém um fabricante pelo Id.")
            .WithDescription("Retorna os dados de um fabricante específico pelo seu identificador.");

            // POST Manufacturer
            group.MapPost("/", async (ManufacturerCreateDTO dto, IManufacturerService service, IValidator<ManufacturerCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/manufacturers/{created.Id}", created);
            })
            .WithName("CreateManufacturer")
            .WithSummary("Cria um novo fabricante.")
            .WithDescription("Adiciona um novo fabricante ao sistema.");

            // DELETE Manufacturer
            group.MapDelete("/{id:guid}", async (Guid id, IManufacturerService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteManufacturer")
            .WithSummary("Remove um fabricante.")
            .WithDescription("Remove um fabricante do sistema pelo seu identificador.");

            return group;
        }
    }
}
