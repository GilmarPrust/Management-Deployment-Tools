using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DCM.Application.DTOs.Application;
using DCM.Application.Services.Interfaces;
using DCM.Infrastructure.Persistence;



namespace DCM.Application.Services.Implementations
{
    public class ApplicationService(AppDbContext context, IMapper mapper, ILogger<ApplicationService> logger) : IApplicationService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ApplicationService> _logger = logger;

        public async Task<IEnumerable<ApplicationReadDTO>> GetAllAsync()
        {
            try
            {
                var apps = await _context.Applications
                    .Include(a => a.Devices)
                    .Include(a => a.DeviceModels)
                    .Include(a => a.DeployProfiles)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ApplicationReadDTO>>(apps);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAllAsync] Erro ao buscar todos os aplicativos.");
                throw;
            }
        }

        public async Task<ApplicationReadDTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var app = await _context.Applications
                    .Include(a => a.Devices)
                    .Include(a => a.DeviceModels)
                    .Include(a => a.DeployProfiles)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return app == null ? null : _mapper.Map<ApplicationReadDTO>(app);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar aplicativo por Id: {Id}", id);
                throw;
            }
        }

        public async Task<ApplicationReadDTO> CreateAsync(ApplicationCreateDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                var entity = _mapper.Map<DCM.Core.Entities.Application>(dto);
                _context.Applications.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Aplicativo criado com Id: {Id}", entity.Id);
                return _mapper.Map<ApplicationReadDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar aplicativo.");
                throw;
            }
        }

        public async Task<ApplicationReadDTO?> UpdateAsync(Guid id, ApplicationUpdateDTO dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                var existing = await _context.Applications.FindAsync(id);
                if (existing == null) return null;

                _mapper.Map(dto, existing);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Aplicativo atualizado: {Id}", id);

                return _mapper.Map<ApplicationReadDTO>(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar aplicativo: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                var entity = await _context.Applications.FindAsync(id);
                if (entity == null) return false;

                _context.Applications.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Aplicativo removido: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover aplicativo: {Id}", id);
                throw;
            }
        }
    }
}
