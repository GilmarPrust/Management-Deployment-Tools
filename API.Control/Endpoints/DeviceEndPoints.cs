using API.Control.DTOs;
using API.Control.Services;

namespace API.Control.Endpoints
{
    public static class DeviceEndpoints
    {
        public static RouteGroupBuilder MapDeviceEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/devices")
                .WithTags("Devices")
                .WithName("DeviceEndpoints")
                .WithSummary("Endpoints for managing devices")
                .WithDescription("Provides endpoints to create, read, update, and delete devices.");

            // GET all
            group.MapGet("/", async (IDeviceService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            });

            // GET by Id
            group.MapGet("/{id:guid}", async (Guid id, IDeviceService service) =>
            {
                var result = await service.GetByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async (DeviceCreateDTO dto, IDeviceService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/devices/{created.Id}", created);
            });

            // DELETE
            group.MapDelete("/{id:guid}", async (Guid id, IDeviceService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
