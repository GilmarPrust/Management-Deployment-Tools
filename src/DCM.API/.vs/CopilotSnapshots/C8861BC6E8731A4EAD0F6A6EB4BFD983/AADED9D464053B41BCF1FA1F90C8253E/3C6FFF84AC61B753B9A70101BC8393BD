using API.Control.DTOs;
using API.Control.DTOs.Firmware;
using API.Control.Models;
using API.Control.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace API.Control.Services.Implementations
{
    public class FirmwareService : IFirmwareService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FirmwareService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FirmwareReadDTO>> GetAllAsync()
        {
            var firmwares = await _context.Firmwares.ToListAsync();
            return _mapper.Map<IEnumerable<FirmwareReadDTO>>(firmwares);
        }

        public async Task<FirmwareReadDTO?> GetByIdAsync(Guid id)
        {
            var firmware = await _context.Firmwares.FindAsync(id);
            return firmware == null ? null : _mapper.Map<FirmwareReadDTO>(firmware);
        }

        public async Task<FirmwareReadDTO> CreateAsync(FirmwareCreateDTO dto)
        {
            var entity = _mapper.Map<Firmware>(dto);

            _context.Firmwares.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<FirmwareReadDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, FirmwareUpdateDTO dto)
        {
            var firmware = await _context.Firmwares.FindAsync(id);
            if (firmware == null) return false;

            _mapper.Map(dto, firmware);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var firmwares = await _context.Firmwares.FindAsync(id);
            if (firmwares == null) return false;

            _context.Firmwares.Remove(firmwares);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
