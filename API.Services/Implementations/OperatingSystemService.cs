namespace DCM.Services.Implementations
{
    public class OperatingSystemService(AppDbContext context, IMapper mapper, ILogger<OperatingSystemService> logger) : IOperatingSystemService
    {
        public async Task<IEnumerable<OperatingSystemReadDTO>> GetAllAsync()
        {
            var operatingSystems = await context.OperatingSystems.OrderBy(os => os.Name).ToListAsync();
            logger.LogInformation("Consulta de todos os sistemas operacionais realizada. Total: {Count}", operatingSystems.Count);
            return mapper.Map<IEnumerable<OperatingSystemReadDTO>>(operatingSystems);
        }

        public async Task<OperatingSystemReadDTO> CreateAsync(OperatingSystemCreateDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var operatingSystem = mapper.Map< DCM.Core.Entities.OperatingSystem >(dto);

            context.OperatingSystems.Add(operatingSystem);
            await context.SaveChangesAsync();

            logger.LogInformation("Sistema operacional criado com sucesso: {Name}", operatingSystem.Name);

            return mapper.Map<OperatingSystemReadDTO>(operatingSystem);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var operatingSystem = await context.OperatingSystems.FindAsync(id);
            if (operatingSystem == null)
            {
                logger.LogWarning("Tentativa de exclusão de sistema operacional não encontrado: {Id}", id);
                return false;
            }

            context.OperatingSystems.Remove(operatingSystem);
            await context.SaveChangesAsync();

            logger.LogInformation("Sistema operacional removido: {Name}", operatingSystem.Name);
            return true;
        }

        public async Task<OperatingSystemReadDTO?> GetByIdAsync(Guid id)
        {
            var operatingSystem = await context.OperatingSystems.FindAsync(id);
            if (operatingSystem == null)
            {
                logger.LogWarning("Sistema operacional não encontrado: {Id}", id);
                return null;
            }

            logger.LogInformation("Consulta de sistema operacional por Id: {Id}", id);
            return mapper.Map<OperatingSystemReadDTO>(operatingSystem);
        }
    }
}
