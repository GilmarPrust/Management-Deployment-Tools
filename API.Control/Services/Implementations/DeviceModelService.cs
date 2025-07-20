using API.Control.DTOs.DeviceModel;
using API.Control.Models;
using API.Control.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Control.Services.Implementations
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceModelService> _logger;

        public DeviceModelService(AppDbContext context, IMapper mapper, ILogger<DeviceModelService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DeviceModelReadDTO>> GetAllAsync()
        {
            try
            {
                var models = await _context.DeviceModels
                    .Include(dm => dm.Firmware)
                    .Include(dm => dm.DriverPacksOEM)
                    .Include(dm => dm.Applications)
                    .Include(dm => dm.Devices)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<DeviceModelReadDTO>>(models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os modelos de dispositivo.");
                throw;
            }
        }

        public async Task<DeviceModelReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var model = await _context.DeviceModels
                    .Include(dm => dm.Firmware)
                    .Include(dm => dm.DriverPacksOEM)
                    .Include(dm => dm.Applications)
                    .Include(dm => dm.Devices)
                    .FirstOrDefaultAsync(dm => dm.Id == id);

                return model == null ? null : _mapper.Map<DeviceModelReadDTO>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar modelo de dispositivo por Id: {Id}", id);
                throw;
            }
        }

        public async Task<DeviceModelReadDTO> CreateAsync(DeviceModelCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var entity = _mapper.Map<DeviceModel>(dto);
                _context.DeviceModels.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Modelo de dispositivo criado com Id: {Id}", entity.Id);
                return _mapper.Map<DeviceModelReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar modelo de dispositivo.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, DeviceModelUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var existing = await _context.DeviceModels.FindAsync(id);
                if (existing == null) return false;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Modelo de dispositivo atualizado: {Id}", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar modelo de dispositivo: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.DeviceModels.FindAsync(id);
                if (entity == null) return false;

                _context.DeviceModels.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Modelo de dispositivo removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover modelo de dispositivo: {Id}", id);
                throw;
            }
        }

        public async Task<bool> AddApplicationsAsync(Guid deviceModelId, List<Guid> applicationIds)
        {
            var deviceModel = await _context.DeviceModels
                .Include(dm => dm.Applications)
                .FirstOrDefaultAsync(dm => dm.Id == deviceModelId);

            if (deviceModel == null) return false;

            var applications = await _context.Applications
                .Where(a => applicationIds.Contains(a.Id))
                .ToListAsync();

            foreach (var app in applications)
            {
                if (!deviceModel.Applications.Contains(app))
                    deviceModel.Applications.Add(app);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> AddDriverPackAsync(Guid id, List<Guid> applicationIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddFirmwareAsync(Guid id, List<Guid> applicationIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddDeviceAsync(Guid id, List<Guid> applicationIds)
        {
            throw new NotImplementedException();
        }
    }
}
