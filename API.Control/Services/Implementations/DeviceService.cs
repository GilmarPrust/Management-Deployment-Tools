using API.Control.DTOs.AppxPackage;
using API.Control.DTOs.Device;
using API.Control.Models;
using API.Control.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Control.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeviceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeviceReadDTO>> GetAllAsync()
        {
            var devices = await _context.Devices
                .Include(d => d.DeviceModel)
                .Include(d => d.ProfileDeploy)
                .Include(d => d.Applications)
                .Include(d => d.DriverPackages)
                .Include(d => d.AppxPackages)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeviceReadDTO>>(devices);
        }

        public async Task<DeviceReadDTO?> GetByIdAsync(Guid id)
        {
            var device = await _context.Devices
                .Include(d => d.DeviceModel)
                .Include(d => d.ProfileDeploy)
                .Include(d => d.Applications)
                .Include(d => d.DriverPackages)
                .Include(d => d.AppxPackages)
                .FirstOrDefaultAsync(d => d.Id == id);

            return device == null ? null : _mapper.Map<DeviceReadDTO>(device);
        }

        public async Task<DeviceReadDTO> CreateAsync(DeviceCreateDTO dto)
        {
            var entity = _mapper.Map<Device>(dto);

            _context.Devices.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DeviceReadDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, DeviceUpdateDTO dto)
        {
            var existing = await _context.Devices.FindAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Devices.FindAsync(id);
            if (entity == null) return false;

            _context.Devices.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
