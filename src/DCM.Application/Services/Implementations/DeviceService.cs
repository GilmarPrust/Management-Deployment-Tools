using AutoMapper;
using DCM.Application.DTOs.Device;
using DCM.Application.Services.Interfaces;
using DCM.Core.Entities;
using DCM.Core.Settings;
using DCM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DCM.Application.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceService> _logger;
        private readonly DatabaseSettings _dbSettings;

        public DeviceService(AppDbContext context, IMapper mapper, ILogger<DeviceService> logger, IOptions<DatabaseSettings> dbSettings)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _dbSettings = dbSettings.Value;
        }

        /*public void Connect()
        {
            Console.WriteLine($"Conectando ao banco: {_dbSettings.ConnectionString}");
        }*/

        public async Task<IEnumerable<DeviceReadDTO>> GetAllAsync()
        {
            var devices = await _context.Devices
                .Include(d => d.DeviceModel)
                .Include(d => d.DeployProfile)
                .Include(d => d.Applications)
                .Include(d => d.DriverPacks)
                .Include(d => d.AppxPackages)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DeviceReadDTO>>(devices);
        }

        public async Task<DeviceReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            var device = await _context.Devices
                .Include(d => d.DeviceModel)
                .Include(d => d.DeployProfile)
                .Include(d => d.Applications)
                .Include(d => d.DriverPacks)
                .Include(d => d.AppxPackages)
                .FirstOrDefaultAsync(d => d.Id == id);

            return device == null ? null : _mapper.Map<DeviceReadDTO>(device);
        }

        public async Task<DeviceReadDTO> CreateAsync(DeviceCreateDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            // Verifica unicidade de SerialNumber
            var serialExists = await _context.Devices.AnyAsync(d => d.SerialNumber == dto.SerialNumber);
            if (serialExists)
                throw new ArgumentException($"Já existe um dispositivo com o mesmo SerialNumber: {dto.SerialNumber}");

            // Verifica unicidade de MacAddress
            var macExists = await _context.Devices.AnyAsync(d => d. MacAddress.Value == dto.MacAddress);
            if (macExists)
                throw new ArgumentException($"Já existe um dispositivo com o mesmo MacAddress: {dto.MacAddress}");

            // Verifica unicidade de ComputerName
            var nameExists = await _context.Devices.AnyAsync(d => d.ComputerName.Value == dto.ComputerName);
            if (nameExists)
                throw new ArgumentException($"Já existe um dispositivo com o mesmo ComputerName: {dto.ComputerName}");

            // Validação: verifica se o DeviceModelId existe
            var deviceModelExists = await _context.DeviceModels.AnyAsync(dm => dm.Id == dto.DeviceModelId);
            if (!deviceModelExists)
                throw new ArgumentException($"DeviceModelId informado não existe: {dto.DeviceModelId}");

            try
            {
                var entity = _mapper.Map<Device>(dto);
                _context.Devices.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dispositivo criado com Id: {Id}", entity.Id);
                return _mapper.Map<DeviceReadDTO>(entity);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro de banco ao criar dispositivo. DTO: {@dto}", dto);
                throw;
            }
        }

        public async Task<DeviceReadDTO?> UpdateAsync(Guid id, DeviceUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            ArgumentNullException.ThrowIfNull(dto);

            var existing = await _context.Devices
                .Include(d => d.DeviceModel)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (existing == null) return null;

            // Verifica unicidade de SerialNumber
            var serialExists = await _context.Devices.AnyAsync(d => d.SerialNumber == dto.SerialNumber && d.Id != id);
            if (serialExists)
                throw new ArgumentException($"Já existe outro dispositivo com o mesmo SerialNumber: {dto.SerialNumber}");

            // Verifica unicidade de MacAddress
            var macExists = await _context.Devices.AnyAsync(d => d.MacAddress.Value == dto.MacAddress && d.Id != id);
            if (macExists)
                throw new ArgumentException($"Já existe outro dispositivo com o mesmo MacAddress: {dto.MacAddress}");

            // Verifica unicidade de ComputerName
            var nameExists = await _context.Devices.AnyAsync(d => d.ComputerName.Value == dto.ComputerName && d.Id != id);
            if (nameExists)
                throw new ArgumentException($"Já existe outro dispositivo com o mesmo ComputerName: {dto.ComputerName}");

            // Validação: verifica se o DeviceModelId existe
            var deviceModelExists = await _context.DeviceModels.AnyAsync(dm => dm.Id == dto.DeviceModelId);
            if (!deviceModelExists)
                throw new ArgumentException($"DeviceModelId informado não existe: {dto.DeviceModelId}");

            try
            {
                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dispositivo atualizado: {Id}", id);
                return _mapper.Map<DeviceReadDTO>(existing);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro de banco ao atualizar dispositivo. Id: {Id}, DTO: {@dto}", id, dto);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            var entity = await _context.Devices.FindAsync(id);
            if (entity == null) return false;

            _context.Devices.Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Dispositivo removido: {Id}", id);
            return true;
        }
    }
}
