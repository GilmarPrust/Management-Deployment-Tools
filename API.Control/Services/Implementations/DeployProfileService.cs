namespace API.Control.Services.Implementations
{
    public class DeployProfileService : IDeployProfileService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeployProfileService> _logger;

        public DeployProfileService(AppDbContext context, IMapper mapper, ILogger<DeployProfileService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DeployProfileReadDTO>> GetAllAsync()
        {
            try
            {
                var profiles = await _context.DeployProfiles
                    .Include(p => p.Image)
                    .Include(p => p.Applications)
                    .Include(p => p.Devices)
                    .Include(p => p.ProfileTasks)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<IEnumerable<DeployProfileReadDTO>>(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAllAsync] Erro ao buscar todos os perfis de implantação.");
                throw;
            }
        }

        public async Task<DeployProfileReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var profile = await _context.DeployProfiles
                    .Include(p => p.Image)
                    .Include(p => p.Applications)
                    .Include(p => p.Devices)
                    .Include(p => p.ProfileTasks)
                    .FirstOrDefaultAsync(p => p.Id == id);

                return profile == null ? null : _mapper.Map<DeployProfileReadDTO>(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar perfil de implantação por Id: {Id}", id);
                throw;
            }
        }

        public async Task<DeployProfileReadDTO> CreateAsync(DeployProfileCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var entity = _mapper.Map<DeployProfile>(dto);
                _context.DeployProfiles.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Perfil de implantação criado com Id: {Id}", entity.Id);
                return _mapper.Map<DeployProfileReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar perfil de implantação.");
                throw;
            }
        }

        public async Task<DeployProfileReadDTO?> UpdateAsync(Guid id, DeployProfileUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var existing = await _context.DeployProfiles
                    .Include(p => p.Applications)
                    .Include(p => p.Devices)
                    .Include(p => p.ProfileTasks)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existing == null) return null;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Perfil de implantação atualizado: {Id}", id);

                return _mapper.Map<DeployProfileReadDTO>(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar perfil de implantação: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.DeployProfiles.FindAsync(id);
                if (entity == null) return false;

                _context.DeployProfiles.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Perfil de implantação removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover perfil de implantação: {Id}", id);
                throw;
            }
        }

        public Task<bool> UpdateDevicesAsync(Guid id, IReadOnlyList<Guid> deviceIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddDeviceAsync(Guid id, Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveDeviceAsync(Guid id, Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<object?> GetDevicesByDeployProfileIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<object?> GetApplicationsByDeployProfileIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateApplicationsAsync(Guid id, object applicationIds)
        {
            throw new NotImplementedException();
        }
    }
}