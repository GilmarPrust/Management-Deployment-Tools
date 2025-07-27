namespace API.Control.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(AppDbContext context, IMapper mapper, ILogger<DeviceService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DeviceReadDTO>> GetAllAsync()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os dispositivos.");
                throw;
            }
        }

        public async Task<DeviceReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var device = await _context.Devices
                    .Include(d => d.DeviceModel)
                    .Include(d => d.DeployProfile)
                    .Include(d => d.Applications)
                    .Include(d => d.DriverPacks)
                    .Include(d => d.AppxPackages)
                    .FirstOrDefaultAsync(d => d.Id == id);

                return device == null ? null : _mapper.Map<DeviceReadDTO>(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar dispositivo por Id: {Id}", id);
                throw;
            }
        }

        public async Task<DeviceReadDTO> CreateAsync(DeviceCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                // Validação: verifica se o DeviceModelId existe
                var deviceModelExists = await _context.DeviceModels.AnyAsync(dm => dm.Id == dto.DeviceModelId);
                if (!deviceModelExists)
                    throw new ArgumentException("DeviceModelId informado não existe.", nameof(dto.DeviceModelId));

                var entity = _mapper.Map<Device>(dto);
                _context.Devices.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dispositivo criado com Id: {Id}", entity.Id);
                return _mapper.Map<DeviceReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar dispositivo.");
                throw;
            }
        }

        public async Task<DeviceReadDTO?> UpdateAsync(Guid id, DeviceUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var existing = await _context.Devices
                    .Include(d => d.DeviceModel)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (existing == null) return null;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dispositivo atualizado: {Id}", id);

                return _mapper.Map<DeviceReadDTO>(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar dispositivo: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.Devices.FindAsync(id);
                if (entity == null) return false;

                _context.Devices.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dispositivo removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover dispositivo: {Id}", id);
                throw;
            }
        }
    }
}
