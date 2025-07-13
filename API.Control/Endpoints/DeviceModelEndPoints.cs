using API.Control.DTOs.DeviceModel;
using API.Control.Services.Interfaces;

namespace API.Control.Endpoints
{
    public static class DeviceModelEndpoints
    {
        public static RouteGroupBuilder MapDeviceModelsEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/devicemodels")
                .WithTags("Device Models")
                .WithName("DeviceModelEndpoints");
            group.WithSummary("Endpoints for managing device models")
                .WithDescription("Provides endpoints to create, read, update, and delete device models.");
            
            group.MapGet("/", async (IDeviceModelService service) =>
                Results.Ok(await service.GetAllAsync()));

            group.MapGet("/{id:guid}", async (IDeviceModelService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            group.MapPost("/", async (IDeviceModelService service, DeviceModelCreateDTO dto) =>
                Results.Ok(await service.CreateAsync(dto)));

            group.MapPut("/", async (Guid id, IDeviceModelService service, DeviceModelUpdateDTO dto) =>
                await service.UpdateAsync(id, dto) ? Results.NoContent() : Results.NotFound());

            group.MapDelete("/{id:guid}", async (IDeviceModelService service, Guid id) =>
                await service.DeleteAsync(id) ? Results.NoContent() : Results.NotFound());

            return group;
        }
    }
}
