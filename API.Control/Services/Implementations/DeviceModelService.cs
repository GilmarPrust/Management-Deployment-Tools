using API.Control.DTOs.Device;
using API.Control.DTOs.DeviceModel;
using API.Control.Models;
using API.Control.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Control.Services.Implementations
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeviceModelService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeviceModelReadDTO>> GetAllAsync()
        {
            var models = await _context.DeviceModels
                .Include(dm => dm.Firmware)
                .Include(dm => dm.DriverPacks)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeviceModelReadDTO>>(models);
        }

        public async Task<DeviceModelReadDTO?> GetByIdAsync(Guid id)
        {
            var model = await _context.DeviceModels
                .Include(dm => dm.Firmware)
                .Include(dm => dm.DriverPacks)
                .FirstOrDefaultAsync(dm => dm.Id == id);

            return model == null ? null : _mapper.Map<DeviceModelReadDTO>(model);
        }

        public async Task<DeviceModelReadDTO> CreateAsync(DeviceModelCreateDTO dto)
        {
            var entity = _mapper.Map<DeviceModel>(dto);
            _context.DeviceModels.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DeviceModelReadDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, DeviceModelUpdateDTO dto)
        {
            var existing = await _context.DeviceModels.FindAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.DeviceModels.FindAsync(id);
            if (entity == null) return false;

            _context.DeviceModels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
