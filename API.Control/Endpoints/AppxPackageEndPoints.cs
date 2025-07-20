using API.Control.DTOs.AppxPackage;
using API.Control.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Control.Endpoints
{
    public static class AppxPackageEndPoints
    {
        public static RouteGroupBuilder MapAppxPackageEndPoints(this RouteGroupBuilder group)
        {
            // GET all
            group.MapGet("/", async ([FromServices] IAppxPackageService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IAppxPackageService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ([FromServices] IAppxPackageService service, AppxPackageCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/appxpackages/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IAppxPackageService service, Guid id, AppxPackageUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IAppxPackageService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}