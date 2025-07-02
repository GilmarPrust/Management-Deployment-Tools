using API.Control.Models;
using API.Control.Services;
using System.ComponentModel.DataAnnotations;

namespace API.Control.Endpoints
{

    public static class DeviceEndpoints
    {
        public static void MapDeviceEndpoints(this WebApplication app)
        {
            // Endpoint para listar todos os dispositivos
            app.MapGet("/device", async (DeviceService service) =>
            {
                return Results.Ok(await service.GetDevices());
            });

            // Endpoint para adicionar um novo dispositivo
            app.MapPost("/device", async (Device device, DeviceService service) =>
            {
                await service.AddDevice(device);
                return Results.Created($"/device/{device.Id}", device);
            });

            // Endpoint para buscar um dispositivo pelo GUID
            app.MapPut("/device/{guid}", async (Guid guid, Device device, DeviceService service) =>
            {
                if (guid != device.Id)
                {
                    return Results.BadRequest("O GUID do dispositivo não corresponde ao GUID fornecido na URL.");
                }
                await service.GetDeviceById(guid);
                return Results.Created( $"/device/{device.Id}", device);
            });

            // Endpoint para remover um dispositivo pelo GUID
            app.MapDelete("/device/{guid}", async (Guid guid, DeviceService service) =>
            {
                await service.RemoveDevice(guid);
                Results.NoContent();
            });
        }
    }
}
