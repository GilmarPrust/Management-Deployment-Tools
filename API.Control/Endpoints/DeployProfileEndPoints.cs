using API.Control.DTOs.DeployProfile.Applications;

namespace API.Control.Endpoints
{
    public static class DeployProfileEndpoints
    {
        public static RouteGroupBuilder MapDeployProfileEndpoints(this RouteGroupBuilder group)
        {
            group.MapGroup("/api/deployprofiles")
                .WithTags("Deploy Profiles")
                .WithName("DeployProfileEndpoints")
                .WithSummary("Endpoints for managing Deploy Profiles")
                .WithDescription("Provides endpoints to create, read, update, and delete deploy profiles.");

            // GET all
            group.MapGet("/", async ([FromServices] IDeployProfileService service) =>
                Results.Ok(await service.GetAllAsync()));

            // GET by Id
            group.MapGet("/{id:guid}", async ([FromServices] IDeployProfileService service, Guid id) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            // POST
            group.MapPost("/", async ([FromServices] IDeployProfileService service, DeployProfileCreateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/deployprofiles/{created.Id}", created);
            });

            // PUT (atualização)
            group.MapPut("/{id:guid}", async ([FromServices] IDeployProfileService service, Guid id, DeployProfileUpdateDTO dto) =>
            {
                if (dto == null)
                    return Results.BadRequest("Dados obrigatórios não informados.");
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:guid}", async ([FromServices] IDeployProfileService service, Guid id) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });


            // GET - Applications by DeployProfileId

            group.MapGet("/{id:guid}/applications", async (Guid id, [FromServices] IDeployProfileService service) =>
            {
                var applications = await service.GetApplicationsByDeployProfileIdAsync(id);
                return applications is not null ? Results.Ok(applications) : Results.NotFound();
            });
            


            // GET - Devices by DeployProfileId
            group.MapGet("/{id:guid}/devices", async (Guid id, [FromServices] IDeployProfileService service) =>
            {
                var devices = await service.GetDevicesByDeployProfileIdAsync(id);
                return devices is not null ? Results.Ok(devices) : Results.NotFound();
            });
            // PUT - Update devices
            group.MapPut("/{id:guid}/devices", async (Guid id, AppxPackageDevicesUpdateDTO dto, [FromServices] IDeployProfileService service) =>
            {
                var result = await service.UpdateDevicesAsync(id, dto.DeviceIds);
                return result ? Results.NoContent() : Results.NotFound();
            });
            // PUT - Add device
            group.MapPut("/{id:guid}/device", async (Guid id, AppxPackageDevicesAddDTO dto, [FromServices] IDeployProfileService service) =>
            {
                var result = await service.AddDeviceAsync(id, dto.DeviceId);
                return result ? Results.NoContent() : Results.NotFound();
            });
            // DELETE - Remove device
            group.MapDelete("/{id:guid}/device/{deviceId:guid}", async (Guid id, Guid deviceId, [FromServices] IDeployProfileService service) =>
            {
                var result = await service.RemoveDeviceAsync(id, deviceId);
                return result ? Results.NoContent() : Results.NotFound();
            });

            return group;
        }
    }
}
