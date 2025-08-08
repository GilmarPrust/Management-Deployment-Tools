namespace DCM.Services.Implementations
{
    public class ProfileTaskService : IProfileTaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileTaskService> _logger;

        public ProfileTaskService(AppDbContext context, IMapper mapper, ILogger<ProfileTaskService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProfileTaskReadDTO>> GetAllAsync()
        {
            try
            {
                var profiles = await _context.ProfileTasks
                    .Include(p => p.DeployProfiles)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ProfileTaskReadDTO>>(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os perfis de implantação.");
                throw;
            }
        }

        public async Task<ProfileTaskReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var profile = await _context.ProfileTasks
                    .Include(p => p.DeployProfiles)
                    .FirstOrDefaultAsync(p => p.Id == id);

                return profile == null ? null : _mapper.Map<ProfileTaskReadDTO>(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar perfil de implantação por Id: {Id}", id);
                throw;
            }
        }

        public async Task<ProfileTaskReadDTO> CreateAsync(ProfileTaskCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var entity = _mapper.Map<DeployProfile>(dto);
                _context.DeployProfiles.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Perfil de implantação criado com Id: {Id}", entity.Id);
                return _mapper.Map<ProfileTaskReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar perfil de implantação.");
                throw;
            }
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.ProfileTasks.FindAsync(id);
                if (entity == null) return false;

                _context.ProfileTasks.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Tarefa de perfil removido: {Id}", id);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover perfil de implantação: {Id}", id);
                throw;
            }
        }

        

        public Task<bool> UpdateAsync(Guid id, ProfileTaskUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}