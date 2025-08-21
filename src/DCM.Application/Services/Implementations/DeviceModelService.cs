using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DCM.Application.DTOs.DeviceModel;
using DCM.Application.Services.Interfaces;
using DCM.Core.Interfaces.Repositories;
using DCM.Core.Entities;

namespace DCM.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para gerenciamento de modelos de dispositivo.
    /// </summary>
    public sealed class DeviceModelService : IDeviceModelService
    {
        private readonly IDeviceModelRepository _deviceModelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceModelService> _logger;

        public DeviceModelService(
            IDeviceModelRepository deviceModelRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DeviceModelService> logger)
        {
            _deviceModelRepository = deviceModelRepository ?? throw new ArgumentNullException(nameof(deviceModelRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DeviceModelReadDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Iniciando busca de todos os modelos de dispositivo");
                
                var models = await _deviceModelRepository.GetWithIncludes(
                    dm => dm.Devices,
                    dm => dm.DriverPacks,
                    dm => dm.Firmware,
                    dm => dm.ApplicationGroups,
                    dm => dm.AppxPackageGroup)
                    .ToListAsync(cancellationToken);
                
                var result = _mapper.Map<IEnumerable<DeviceModelReadDTO>>(models);
                
                _logger.LogInformation("Retornados {Count} modelos de dispositivo", result.Count());
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os modelos de dispositivo");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<DeviceModelReadDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));

            try
            {
                _logger.LogDebug("Buscando modelo de dispositivo com Id: {Id}", id);
                
                var model = await _deviceModelRepository.GetWithIncludes(
                    dm => dm.Devices,
                    dm => dm.DriverPacks,
                    dm => dm.Firmware,
                    dm => dm.ApplicationGroups,
                    dm => dm.AppxPackageGroup)
                    .FirstOrDefaultAsync(dm => dm.Id == id, cancellationToken);

                if (model == null)
                {
                    _logger.LogWarning("Modelo de dispositivo não encontrado com Id: {Id}", id);
                    return null;
                }

                var result = _mapper.Map<DeviceModelReadDTO>(model);
                _logger.LogDebug("Modelo de dispositivo encontrado: {Manufacturer} {Model}", model.Manufacturer, model.Model);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar modelo de dispositivo por Id: {Id}", id);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<DeviceModelReadDTO?> GetByManufacturerAndModelAsync(string manufacturer, string model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new ArgumentException("Fabricante não pode ser nulo ou vazio.", nameof(manufacturer));
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Modelo não pode ser nulo ou vazio.", nameof(model));

            try
            {
                _logger.LogDebug("Buscando modelo de dispositivo: {Manufacturer} {Model}", manufacturer, model);
                
                var deviceModel = await _deviceModelRepository.GetByManufacturerAndModelAsync(manufacturer, model, cancellationToken);
                if (deviceModel == null)
                {
                    _logger.LogWarning("Modelo de dispositivo não encontrado: {Manufacturer} {Model}", manufacturer, model);
                    return null;
                }

                // Carregar entidades relacionadas
                var modelWithIncludes = await _deviceModelRepository.GetWithIncludes(
                    dm => dm.Devices,
                    dm => dm.DriverPacks,
                    dm => dm.Firmware,
                    dm => dm.ApplicationGroups,
                    dm => dm.AppxPackageGroup)
                    .FirstOrDefaultAsync(dm => dm.Id == deviceModel.Id, cancellationToken);

                var result = _mapper.Map<DeviceModelReadDTO>(modelWithIncludes);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar modelo de dispositivo: {Manufacturer} {Model}", manufacturer, model);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DeviceModelReadDTO>> GetByManufacturerAsync(string manufacturer, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(manufacturer))
                return Enumerable.Empty<DeviceModelReadDTO>();

            try
            {
                _logger.LogDebug("Buscando modelos de dispositivo do fabricante: {Manufacturer}", manufacturer);
                
                var models = await _deviceModelRepository.GetByManufacturerAsync(manufacturer, cancellationToken);
                var result = _mapper.Map<IEnumerable<DeviceModelReadDTO>>(models);
                
                _logger.LogInformation("Encontrados {Count} modelos do fabricante {Manufacturer}", result.Count(), manufacturer);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar modelos de dispositivo do fabricante: {Manufacturer}", manufacturer);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DeviceModelReadDTO>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Buscando todos os modelos de dispositivo ativos");
                
                var models = await _deviceModelRepository
                .GetWithIncludes(
                    dm => dm.Devices,
                    dm => dm.DriverPacks,
                    dm => dm.Firmware,
                    dm => dm.ApplicationGroups,
                    dm => dm.AppxPackageGroup)
                .Where(dm => dm.Enabled)
                .ToListAsync(cancellationToken);
                
                var result = _mapper.Map<IEnumerable<DeviceModelReadDTO>>(models);
                
                _logger.LogInformation("Retornados {Count} modelos de dispositivo ativos", result.Count());
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar modelos de dispositivo ativos");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<DeviceModelReadDTO> CreateAsync(DeviceModelCreateDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                _logger.LogDebug("Criando novo modelo de dispositivo: {Manufacturer} {Model}", dto.Manufacturer, dto.Model);

                // Validação de negócio: Verificar duplicidade por fabricante e modelo
                if (!string.IsNullOrWhiteSpace(dto.Manufacturer) && !string.IsNullOrWhiteSpace(dto.Model))
                {
                    var existingModel = await _deviceModelRepository.GetByManufacturerAndModelAsync(dto.Manufacturer, dto.Model, cancellationToken);
                    if (existingModel != null)
                    {
                        throw new InvalidOperationException($"Já existe um modelo de dispositivo: {dto.Manufacturer} {dto.Model}");
                    }
                }

                var deviceModel = _mapper.Map<DeviceModel>(dto);
                
                _deviceModelRepository.Add(deviceModel);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Modelo de dispositivo criado com sucesso. Id: {Id}, Modelo: {Manufacturer} {Model}", 
                    deviceModel.Id, deviceModel.Manufacturer, deviceModel.Model);
                
                return _mapper.Map<DeviceModelReadDTO>(deviceModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar modelo de dispositivo: {Manufacturer} {Model}", dto.Manufacturer, dto.Model);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<DeviceModelReadDTO?> UpdateAsync(Guid id, DeviceModelUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id não pode ser vazio.", nameof(id));
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                _logger.LogDebug("Atualizando modelo de dispositivo: {Id}", id);

                var existing = await _deviceModelRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                {
                    _logger.LogWarning("Modelo de dispositivo não encontrado para atualização: {Id}", id);
                    return null;
                }

                // Validação de negócio: Verificar duplicidade por fabricante e modelo (se mudou)
                if (!string.IsNullOrWhiteSpace(dto.Manufacturer) && !string.IsNullOrWhiteSpace(dto.Model) &&
                    (dto.Manufacturer != existing.Manufacturer || dto.Model != existing.Model))
                {
                    var duplicateModel = await _deviceModelRepository.GetByManufacturerAndModelAsync(dto.Manufacturer, dto.Model, cancellationToken);
                    if (duplicateModel != null)
                    {
                        throw new InvalidOperationException($"Já existe um modelo de dispositivo: {dto.Manufacturer} {dto.Model}");
                    }
                }

                _mapper.Map(dto, existing);

                _deviceModelRepository.Update(existing);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Modelo de dispositivo atualizado com sucesso: {Id}", id);
                return _mapper.Map<DeviceModelReadDTO>(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar modelo de dispositivo: {Id}", id);
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
                _logger.LogDebug("Removendo modelo de dispositivo: {Id}", id);

                var deviceModel = await _deviceModelRepository.GetByIdAsync(id, cancellationToken);
                if (deviceModel == null)
                {
                    _logger.LogWarning("Modelo de dispositivo não encontrado para remoção: {Id}", id);
                    return false;
                }

                // Soft delete
                deviceModel.SoftDelete();
                
                _deviceModelRepository.Update(deviceModel);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Modelo de dispositivo removido com sucesso: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover modelo de dispositivo: {Id}", id);
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
                return await _deviceModelRepository.ExistsAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do modelo de dispositivo: {Id}", id);
                throw;
            }
        }

        public Task<IEnumerable<DeviceModelReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DeviceModelReadDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceModelReadDTO> CreateAsync(DeviceModelCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceModelReadDTO?> UpdateAsync(Guid id, DeviceModelUpdateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddApplicationsAsync(Guid id, List<Guid> applicationIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddDriverPackAsync(Guid id, List<Guid> DriverPackIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddFirmwareAsync(Guid id, List<Guid> FirmwareIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddDeviceAsync(Guid id, List<Guid> DeviceIds)
        {
            throw new NotImplementedException();
        }
    }
}
