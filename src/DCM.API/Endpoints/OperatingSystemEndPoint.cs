using DCM.Application.Services.Interfaces;
using DCM.Application.DTOs.OperatingSystem;

namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de sistemas operacionais.
    /// </summary>
    public static class OperatingSystemEndPoint
    {
        /// <summary>
        /// Mapeia os endpoints de OperatingSystem no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapOperatingSystemEndpoints(this RouteGroupBuilder group)
        {
            // GET all Operating Systems
            group.MapGet("/", async (IOperatingSystemService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllOperatingSystems")
            .WithSummary("Obtém todos os sistemas operacionais.")
            .WithDescription("Retorna uma lista de todos os sistemas operacionais cadastrados.");

            // GET Operating System by Id
            group.MapGet("/{id:guid}", async (Guid id, IOperatingSystemService service) =>
            {
                var result = await service.GetByIdAsync(id);
                return result is not null
                    ? Results.Ok(result)
                    : Results.NotFound();
            })
            .WithName("GetOperatingSystemById")
            .WithSummary("Obtém um sistema operacional pelo Id.")
            .WithDescription("Retorna os dados de um sistema operacional específico pelo seu identificador.");

            // POST Operating System
            group.MapPost("/", async (OperatingSystemCreateDTO dto, IOperatingSystemService service, IValidator<OperatingSystemCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/operatingsystems/{created.Id}", created);
            })
            .WithName("CreateOperatingSystem")
            .WithSummary("Cria um novo sistema operacional.")
            .WithDescription("Adiciona um novo sistema operacional ao sistema.");

            // DELETE Operating System
            group.MapDelete("/{id:guid}", async (Guid id, IOperatingSystemService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted
                    ? Results.NoContent()
                    : Results.NotFound();
            })
            .WithName("DeleteOperatingSystem")
            .WithSummary("Remove um sistema operacional.")
            .WithDescription("Remove um sistema operacional do sistema pelo seu identificador.");

            return group;
        }
    }
}
