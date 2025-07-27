namespace API.Control.Services.Implementations
{
    public class DriverPackOEMService : IDriverPackOEMService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DriverPackOEMService> _logger;

        public DriverPackOEMService(AppDbContext context, IMapper mapper, ILogger<DriverPackOEMService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DriverPackOEMReadDTO>> GetAllAsync()
        {
            try
            {
                var packages = await _context.DriverPacks
                    .ToListAsync();

                return _mapper.Map<IEnumerable<DriverPackOEMReadDTO>>(packages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os DriverPackages.");
                throw;
            }
        }

        public async Task<DriverPackOEMReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var package = await _context.DriverPacks
                    .FirstOrDefaultAsync(dp => dp.Id == id);

                return package == null ? null : _mapper.Map<DriverPackOEMReadDTO>(package);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar DriverPackage por Id: {Id}", id);
                throw;
            }
        }

        public async Task<DriverPackOEMReadDTO> CreateAsync(DriverPackOEMCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var entity = _mapper.Map<DriverPack>(dto);
                _context.DriverPacks.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("DriverPack criado com Id: {Id}", entity.Id);
                return _mapper.Map<DriverPackOEMReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar DriverPack.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, DriverPackOEMUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            try
            {
                var existing = await _context.DriverPacks.FindAsync(id);
                if (existing == null) return false;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("DriverPack atualizado: {Id}", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar DriverPack: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.DriverPacks.FindAsync(id);
                if (entity == null) return false;

                _context.DriverPacks.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("DriverPack removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover DriverPack: {Id}", id);
                throw;
            }
        }
    }
}