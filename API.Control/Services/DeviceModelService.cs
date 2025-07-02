using API.Control.Data;
using API.Control.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Control.Services
{
    public class DeviceModelService
    {
        private readonly AppDbContext _context;

        public DeviceModelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeviceModel>> G()
        {
            return await _context.DeviceModel.ToListAsync();
        }

        public async Task AdicionarAsync(DeviceModel devicemodel)
        {
            _context.DeviceModel.Add(devicemodel);
            await _context.SaveChangesAsync();
        }

        // Outros métodos: Atualizar, Remover, etc.
    }
}
