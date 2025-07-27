namespace API.Control.Services.Implementations
{
    public class AppxPackageService : IAppxPackageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AppxPackageService> _logger;

        public AppxPackageService(AppDbContext context, IMapper mapper, ILogger<AppxPackageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AppxPackageReadDTO>> GetAllAsync()
        {
            try
            {
                var packages = await _context.AppxPackages
                    .Include(a => a.Devices)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<IEnumerable<AppxPackageReadDTO>>(packages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAllAsync] Erro ao buscar todos os pacotes Appx.");
                throw;
            }
        }

        public async Task<AppxPackageReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var package = await _context.AppxPackages
                    .Include(a => a.Devices)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return package == null ? null : _mapper.Map<AppxPackageReadDTO>(package);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pacote Appx por Id: {Id}", id);
                throw;
            }
        }

        public async Task<AppxPackageReadDTO> CreateAsync(AppxPackageCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var entity = _mapper.Map<AppxPackage>(dto);
                _context.AppxPackages.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pacote Appx criado com Id: {Id}", entity.Id);
                return _mapper.Map<AppxPackageReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pacote Appx.");
                throw;
            }
        }

        public async Task<AppxPackageReadDTO?> UpdateAsync(Guid id, AppxPackageUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var existing = await _context.AppxPackages.FindAsync(id);
                if (existing == null) return null;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pacote Appx atualizado: {Id}", id);

                return _mapper.Map<AppxPackageReadDTO>(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pacote Appx: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.AppxPackages.FindAsync(id);
                if (entity == null) return false;

                _context.AppxPackages.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pacote Appx removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover pacote Appx: {Id}", id);
                throw;
            }
        }
    }
}