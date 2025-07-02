using API.Control.Data;
using API.Control.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Control.Services
{
    public class DeviceService
    {
        // Contexto do banco de dados
        private readonly AppDbContext _context;

        // Construtor que recebe o contexto do banco de dados
        public DeviceService(AppDbContext context)
        {
            _context = context;
        }

        // Método para listar todos os dispositivos
        public async Task<List<Device>> GetDevices()
        {
            return await _context.Device.ToListAsync();
        }

        // Método para buscar um device pelo Guid.
        public async Task<Device?> GetDeviceById(Guid Id)
        {
            return await _context.Device.FirstAsync(x => x.Id == Id);
        }

        // Método para buscar um dispositivo pelo GUID
        public async Task AddDevice(Device device)
        {
            _context.Device.Add(device);
            await _context.SaveChangesAsync();
        }

        // Método para atualizar um dispositivo
        public async Task<bool> UpdateDevice(Device device)
        {
            var existingDevice = await _context.Device.FindAsync(device.Id);
            if (existingDevice == null) return false;

            existingDevice.ComputerName = device.ComputerName;
            existingDevice.SerialNumber = device.SerialNumber;
            existingDevice.MACAddress = device.MACAddress;

            _context.Device.Update(existingDevice);
            await _context.SaveChangesAsync();
            return true;
        }

        // Método para remover um dispositivo pelo GUID
        public async Task<bool> RemoveDevice(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            if (device == null) return false;

            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
