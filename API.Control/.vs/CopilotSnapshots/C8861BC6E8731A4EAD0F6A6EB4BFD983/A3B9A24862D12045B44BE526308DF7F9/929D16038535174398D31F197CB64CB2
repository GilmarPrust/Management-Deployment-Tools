using API.Control.DTOs.AppxPackage;
using API.Control.DTOs.Device;
using API.Control.Services.Interfaces;


namespace API.Control.Endpoints
{
    public static class DeviceEndpoints
    {
        public static RouteGroupBuilder MapDeviceEndpoints(this RouteGroupBuilder group)
        {
            // GET all
            group.MapGet("/", async (IDeviceService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async (IDeviceService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is null ? Results.NotFound() : Results.Ok(dto);
            });

            // POST
            group.MapPost("/", async (IDeviceService service, DeviceCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/devices/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async (IDeviceService service, Guid id, DeviceUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async (IDeviceService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
