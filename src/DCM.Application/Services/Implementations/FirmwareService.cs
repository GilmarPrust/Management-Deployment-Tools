using AutoMapper;
using DCM.Application.DTOs.Firmware;
using DCM.Application.Services.Interfaces;
using DCM.Core.Entities;
using DCM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DCM.Application.Services.Implementations
{
    public class FirmwareService : IFirmwareService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FirmwareService> _logger;

        public FirmwareService(AppDbContext context, IMapper mapper, ILogger<FirmwareService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<FirmwareReadDTO>> GetAllAsync()
        {
            try
            {
                var firmwares = await _context.Firmwares
                    .Include(f => f.DeviceModel)
                    .ToListAsync();
                return _mapper.Map<IEnumerable<FirmwareReadDTO>>(firmwares);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os firmwares.");
                throw;
            }
        }

        public async Task<FirmwareReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            try
            {
                var firmware = await _context.Firmwares
                    .Include(f => f.DeviceModel)
                    .FirstOrDefaultAsync(f => f.Id == id);
                return firmware == null ? null : _mapper.Map<FirmwareReadDTO>(firmware);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar firmware por Id: {Id}", id);
                throw;
            }
        }

        public async Task<FirmwareReadDTO> CreateAsync(FirmwareCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            try
            {
                var entity = _mapper.Map<Firmware>(dto);
                _context.Firmwares.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Firmware criado com Id: {Id}", entity.Id);
                return _mapper.Map<FirmwareReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar firmware.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, FirmwareUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            try
            {
                var firmware = await _context.Firmwares.FindAsync(id);
                if (firmware == null) return false;
                _mapper.Map(dto, firmware);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Firmware atualizado: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar firmware: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            try
            {
                var firmware = await _context.Firmwares.FindAsync(id);
                if (firmware == null) return false;
                _context.Firmwares.Remove(firmware);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Firmware removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover firmware: {Id}", id);
                throw;
            }
        }
    }
}
