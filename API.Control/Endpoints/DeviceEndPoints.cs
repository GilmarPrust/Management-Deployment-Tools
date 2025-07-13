using API.Control.DTOs.AppxPackage;
using API.Control.DTOs.Device;
using API.Control.Services.Interfaces;


namespace API.Control.Endpoints
{
    public static class DeviceEndpoints
    {
        public static RouteGroupBuilder MapDeviceEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IDeviceService service) =>
                Results.Ok(await service.GetAllAsync()));

            group.MapGet("/{id:guid}", async (IDeviceService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is null ? Results.NotFound() : Results.Ok(dto);
            });

            group.MapPost("/", async (IDeviceService service, DeviceCreateDTO dto) =>
            {
                var id = await service.CreateAsync(dto);
                return Results.Created($"/api/devices/{id}", id);
            });

            group.MapPut("/{id:guid}", async (IDeviceService service, Guid id, DeviceUpdateDTO dto) =>
            {
                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (IDeviceService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
