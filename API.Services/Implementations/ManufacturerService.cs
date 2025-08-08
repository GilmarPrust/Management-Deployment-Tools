namespace DCM.Services.Implementations
{
    public class ManufacturerService(AppDbContext context, IMapper mapper, ILogger<ManufacturerService> logger) : IManufacturerService
    {
        public async Task<ManufacturerReadDTO> CreateAsync(ManufacturerCreateDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            try
            {
                var manufacturer = mapper.Map<Manufacturer>(dto);
                context.Manufacturers.Add(manufacturer);
                await context.SaveChangesAsync();
                logger.LogInformation("Fabricante criado com sucesso: {Name}", manufacturer.Name);
                return mapper.Map<ManufacturerReadDTO>(manufacturer);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Erro ao persistir fabricante: {DTO}", dto);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var manufacturer = await context.Manufacturers.FindAsync(id);
                if (manufacturer == null)
                {
                    logger.LogWarning("Tentativa de exclusão de fabricante não encontrado: {Id}", id);
                    return false;
                }
                context.Manufacturers.Remove(manufacturer);
                await context.SaveChangesAsync();
                logger.LogInformation("Fabricante removido: {Name}", manufacturer.Name);
                return true;
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Erro ao remover fabricante: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<ManufacturerReadDTO>> GetAllAsync()
        {
            var manufacturers = await context.Manufacturers.AsNoTracking().ToListAsync();
            logger.LogInformation("Consulta de todos os fabricantes realizada. Total: {Count}", manufacturers.Count);
            return mapper.Map<IEnumerable<ManufacturerReadDTO>>(manufacturers);
        }

        public async Task<ManufacturerReadDTO?> GetByIdAsync(Guid id)
        {
            var manufacturer = await context.Manufacturers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                logger.LogWarning("Fabricante não encontrado: {Id}", id);
                return null;
            }
            logger.LogInformation("Consulta de fabricante por Id: {Id}", id);
            return mapper.Map<ManufacturerReadDTO>(manufacturer);
        }
    }
}
