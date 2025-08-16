using DCM.Application.DTOs.Device;

namespace DCM.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para gerenciamento de dispositivos.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Obtém todos os dispositivos ativos com suas relações.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de dispositivos.</returns>
        Task<IEnumerable<DeviceReadDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém um dispositivo por ID com suas relações.
        /// </summary>
        /// <param name="id">ID do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dispositivo encontrado ou null.</returns>
        Task<DeviceReadDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém um dispositivo por nome do computador.
        /// </summary>
        /// <param name="computerName">Nome do computador.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dispositivo encontrado ou null.</returns>
        Task<DeviceReadDTO?> GetByComputerNameAsync(string computerName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém um dispositivo por endereço MAC.
        /// </summary>
        /// <param name="macAddress">Endereço MAC do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dispositivo encontrado ou null.</returns>
        Task<DeviceReadDTO?> GetByMacAddressAsync(string macAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém um dispositivo por número de série.
        /// </summary>
        /// <param name="serialNumber">Número de série do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dispositivo encontrado ou null.</returns>
        Task<DeviceReadDTO?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém dispositivos por modelo.
        /// </summary>
        /// <param name="deviceModelId">ID do modelo do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de dispositivos do modelo especificado.</returns>
        Task<IEnumerable<DeviceReadDTO>> GetByDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria um novo dispositivo.
        /// </summary>
        /// <param name="dto">Dados para criação do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dispositivo criado.</returns>
        Task<DeviceReadDTO> CreateAsync(DeviceCreateDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Atualiza um dispositivo existente.
        /// </summary>
        /// <param name="id">ID do dispositivo a ser atualizado.</param>
        /// <param name="dto">Dados para atualização do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Dispositivo atualizado ou null se não encontrado.</returns>
        Task<DeviceReadDTO?> UpdateAsync(Guid id, DeviceUpdateDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove um dispositivo (soft delete).
        /// </summary>
        /// <param name="id">ID do dispositivo a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se removido com sucesso, false se não encontrado.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se um dispositivo existe.
        /// </summary>
        /// <param name="id">ID do dispositivo.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se existe, false caso contrário.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
