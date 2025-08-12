using DCM.Application.Services.Interfaces;
using DCM.Application.DTOs.ProfileTask;

namespace DCM.API.Endpoints
{
    /// <summary>
    /// Endpoints para operações CRUD de tarefas de perfil.
    /// </summary>
    public static class ProfileTaskEndPoint
    {
        /// <summary>
        /// Mapeia os endpoints de ProfileTask no grupo fornecido.
        /// </summary>
        public static RouteGroupBuilder MapProfileTaskEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/profiletasks")
                .WithTags("Profile Tasks")
                .WithName("ProfileTaskEndpoints")
                .WithSummary("Endpoints for managing Profile Tasks")
                .WithDescription("Provides endpoints to create, read, update, and delete profile tasks.");

            // GET all ProfileTasks
            group.MapGet("/", async (IProfileTaskService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            })
            .WithName("GetAllProfileTasks")
            .WithSummary("Obtém todas as tarefas de perfil.")
            .WithDescription("Retorna uma lista de todas as tarefas de perfil cadastradas.");

            // GET ProfileTask by Id
            group.MapGet("/{id:guid}", async (Guid id, IProfileTaskService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetProfileTaskById")
            .WithSummary("Obtém uma tarefa de perfil pelo Id.")
            .WithDescription("Retorna os dados de uma tarefa de perfil específica pelo seu identificador.");

            // POST ProfileTask
            group.MapPost("/", async (ProfileTaskCreateDTO dto, IProfileTaskService service, IValidator<ProfileTaskCreateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/profiletasks/{created.Id}", created);
            })
            .WithName("CreateProfileTask")
            .WithSummary("Cria uma nova tarefa de perfil.")
            .WithDescription("Adiciona uma nova tarefa de perfil ao sistema.");

            // PUT ProfileTask
            group.MapPut("/{id:guid}", async (Guid id, ProfileTaskUpdateDTO dto, IProfileTaskService service, IValidator<ProfileTaskUpdateDTO> validator) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");

                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var updated = await service.UpdateAsync(id, dto);
                return updated ? Results.Ok() : Results.NotFound();
            })
            .WithName("UpdateProfileTask")
            .WithSummary("Atualiza uma tarefa de perfil existente.")
            .WithDescription("Atualiza os dados de uma tarefa de perfil pelo seu identificador.");

            // DELETE ProfileTask
            group.MapDelete("/{id:guid}", async (Guid id, IProfileTaskService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteProfileTask")
            .WithSummary("Remove uma tarefa de perfil.")
            .WithDescription("Remove uma tarefa de perfil do sistema pelo seu identificador.");

            return group;
        }
    }
}
