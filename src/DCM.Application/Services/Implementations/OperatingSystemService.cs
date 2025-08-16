using AutoMapper;
using DCM.Application.DTOs.OperatingSystem;
using DCM.Application.Services.Interfaces;
using DCM.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace DCM.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para gerenciamento de sistemas operacionais.
    /// </summary>
    public sealed class OperatingSystemService : IOperatingSystemService
    {
        private readonly IOperatingSystemRepository _operatingSystemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OperatingSystemService> _logger;

        public OperatingSystemService(
            IOperatingSystemRepository operatingSystemRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<OperatingSystemService> logger)
        {
            _operatingSystemRepository = operatingSystemRepository ?? throw new ArgumentNullException(nameof(operatingSystemRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<OperatingSystemReadDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Iniciando busca de todos os sistemas operacionais");
                
                var operatingSystems = await _operatingSystemRepository.GetAllActiveAsync(cancellationToken);
                var result = _mapper.Map<IEnumerable<OperatingSystemReadDTO>>(operatingSystems);
                
                _logger.LogInformation("Retornados {Count} sistemas operacionais", result.Count());
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os sistemas operacionais");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OperatingSystemReadDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                _logger.LogDebug("Buscando sistema operacional com Id: {Id}", id);
                
                var operatingSystem = await _operatingSystemRepository.GetByIdAsync(id, cancellationToken);
                if (operatingSystem == null)
                {
                    _logger.LogWarning("Sistema operacional não encontrado com Id: {Id}", id);
                    return null;
                }

                var result = _mapper.Map<OperatingSystemReadDTO>(operatingSystem);
                _logger.LogDebug("Sistema operacional encontrado: {Name}", operatingSystem.Name);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sistema operacional por Id: {Id}", id);
                throw;
            }
        }


        /// <inheritdoc/>
        public async Task<OperatingSystemReadDTO> CreateAsync(OperatingSystemCreateDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                _logger.LogDebug("Criando novo sistema operacional: {Name}", dto.Name);

                // Validação de negócio: Verificar duplicidade por ShortName
                if (!string.IsNullOrWhiteSpace(dto.ShortName))
                {
                    var existingOS = await _operatingSystemRepository.GetByShortNameAsync(dto.ShortName, cancellationToken);
                    if (existingOS != null)
                    {
                        throw new InvalidOperationException($"Já existe um sistema operacional com ShortName: {dto.ShortName}");
                    }
                }

                var operatingSystem = _mapper.Map<Core.Entities.OperatingSystem>(dto);
                
                await _operatingSystemRepository.AddAsync(operatingSystem, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Sistema operacional criado com sucesso. Id: {Id}, Name: {Name}", operatingSystem.Id, operatingSystem.Name);
                
                return _mapper.Map<OperatingSystemReadDTO>(operatingSystem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar sistema operacional: {Name}", dto.Name);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                _logger.LogDebug("Removendo sistema operacional: {Id}", id);

                var operatingSystem = await _operatingSystemRepository.GetByIdAsync(id, cancellationToken);
                if (operatingSystem == null)
                {
                    _logger.LogWarning("Sistema operacional não encontrado para remoção: {Id}", id);
                    return false;
                }

                // Soft delete
                operatingSystem.SoftDelete();
                
                await _operatingSystemRepository.UpdateAsync(operatingSystem, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Sistema operacional removido com sucesso: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover sistema operacional: {Id}", id);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                return await _operatingSystemRepository.ExistsAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do sistema operacional: {Id}", id);
                throw;
            }
        }

        public Task<OperatingSystemReadDTO?> GetByShortNameAsync(string shortName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OperatingSystemReadDTO>> GetSupportedVersionsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
