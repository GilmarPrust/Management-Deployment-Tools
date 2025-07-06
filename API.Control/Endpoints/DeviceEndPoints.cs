using API.Control.Models;

namespace API.Control.Endpoints
{
    public static class DeviceEndPoints
    {
        // In-memory storage for devices (for demonstration purposes only, not suitable for production use)
        private static readonly List<Device> device = new();

        public static void MapDeviceEndpoints(this IEndpointRouteBuilder app)
        {
            // Define the API group for devices
            var group = app.MapGroup("/api/devices");

            // Initialize with some sample data
            group.MapGet("/", () => Results.Ok(device));

            // Get all devices
            group.MapPost("/", (Device appData) =>
            {
                device.Add(appData);
                return Results.Created($"/api/devices/{appData.Id}", appData);
            });

            // Get a device by ID
            group.MapGet("/{id:guid}", (Guid id) =>
            {
                var app = device.FirstOrDefault(a => a.Id == id);
                return app is not null ? Results.Ok(app) : Results.NotFound();
            });

            // Update a device by ID
            group.MapDelete("/{id:guid}", (Guid id) =>
            {
                var app = device.FirstOrDefault(a => a.Id == id);
                if (app is null) return Results.NotFound();
                device.Remove(app);
                return Results.NoContent();
            });
        }
    }
}
