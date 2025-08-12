namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de inventário.
    /// </summary>
    public static class InventoryEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de Inventory no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapInventoryEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/inventories")
                .WithTags("Inventory")
                .WithName("InventoryEndpoints")
                .WithSummary("Endpoints for managing inventory items")
                .WithDescription("Provides endpoints to create, read, update, and delete inventory items.");

            // GET all Inventories
            group.MapGet("/", async (IInventoryService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllInventories")
            .WithSummary("Obtém todos os inventários.")
            .WithDescription("Retorna uma lista de todos os inventários cadastrados.");

            // GET Inventory by Id
            group.MapGet("/{id:guid}", async (Guid id, IInventoryService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetInventoryById")
            .WithSummary("Obtém um inventário pelo Id.")
            .WithDescription("Retorna os dados de um inventário específico pelo seu identificador.");

            // POST Inventory
            group.MapPost("/", async (InventoryCreateDTO dto, IInventoryService service, IValidator<InventoryCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/inventories/{created.Id}", created);
            })
            .WithName("CreateInventory")
            .WithSummary("Cria um novo inventário.")
            .WithDescription("Adiciona um novo inventário ao sistema.");

            // DELETE Inventory
            group.MapDelete("/{id:guid}", async (Guid id, IInventoryService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteInventory")
            .WithSummary("Remove um inventário.")
            .WithDescription("Remove um inventário do sistema pelo seu identificador.");

            return group;
        }
    }
}
