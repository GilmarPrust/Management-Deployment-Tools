namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de DriverPack.
    /// </summary>
    public static class DriverPackEndpoints
    {
        /// <summary>
        /// Mapeia os endpoints de DriverPack no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapDriverPackEndpoints(this RouteGroupBuilder group)
        {
            // GET all DriverPacks
            group.MapGet("/", async (IDriverPackService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllDriverPacks")
            .WithSummary("Obtém todos os pacotes de driver.")
            .WithDescription("Retorna uma lista de todos os pacotes de driver cadastrados.");

            // GET DriverPack by Id
            group.MapGet("/{id:guid}", async (Guid id, IDriverPackService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetDriverPackById")
            .WithSummary("Obtém um pacote de driver pelo Id.")
            .WithDescription("Retorna os dados de um pacote de driver específico pelo seu identificador.");

            // POST DriverPack
            group.MapPost("/", async (DriverPackCreateDTO dto, IDriverPackService service, IValidator<DriverPackCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/driverpacks/{created.Id}", created);
            })
            .WithName("CreateDriverPack")
            .WithSummary("Cria um novo pacote de driver.")
            .WithDescription("Adiciona um novo pacote de driver ao sistema.");

            // PUT DriverPack
            group.MapPut("/{id:guid}", async (Guid id, DriverPackUpdateDTO dto, IDriverPackService service, IValidator<DriverPackUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateDriverPack")
            .WithSummary("Atualiza um pacote de driver existente.")
            .WithDescription("Atualiza os dados de um pacote de driver pelo seu identificador.");

            // DELETE DriverPack
            group.MapDelete("/{id:guid}", async (Guid id, IDriverPackService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteDriverPack")
            .WithSummary("Remove um pacote de driver.")
            .WithDescription("Remove um pacote de driver do sistema pelo seu identificador.");

            return group;
        }
    }
}
