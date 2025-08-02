namespace API.Control.Services.Implementations
{
    public class OperatingSystemService : IOperatingSystemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OperatingSystemService> _logger;

        public OperatingSystemService(AppDbContext context, IMapper mapper, ILogger<OperatingSystemService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OperatingSystemReadDTO>> GetAllAsync()
        {
            var operatingSystems = await _context.OperatingSystems.OrderBy(os => os.Name).ToListAsync();
            _logger.LogInformation("Consulta de todos os sistemas operacionais realizada. Total: {Count}", operatingSystems.Count);
            return _mapper.Map<IEnumerable<OperatingSystemReadDTO>>(operatingSystems);
        }

        public async Task<OperatingSystemReadDTO> CreateAsync(OperatingSystemCreateDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var operatingSystem = _mapper.Map<API.Control.Entities.Auxiliary.OperatingSystem>(dto);

            _context.OperatingSystems.Add(operatingSystem);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Sistema operacional criado com sucesso: {Name}", operatingSystem.Name);

            return _mapper.Map<OperatingSystemReadDTO>(operatingSystem);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var operatingSystem = await _context.OperatingSystems.FindAsync(id);
            if (operatingSystem == null)
            {
                _logger.LogWarning("Tentativa de exclusão de sistema operacional não encontrado: {Id}", id);
                return false;
            }

            _context.OperatingSystems.Remove(operatingSystem);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Sistema operacional removido: {Name}", operatingSystem.Name);
            return true;
        }

        public async Task<OperatingSystemReadDTO?> GetByIdAsync(Guid id)
        {
            var operatingSystem = await _context.OperatingSystems.FindAsync(id);
            if (operatingSystem == null)
            {
                _logger.LogWarning("Sistema operacional não encontrado: {Id}", id);
                return null;
            }

            _logger.LogInformation("Consulta de sistema operacional por Id: {Id}", id);
            return _mapper.Map<OperatingSystemReadDTO>(operatingSystem);
        }
    }
}
