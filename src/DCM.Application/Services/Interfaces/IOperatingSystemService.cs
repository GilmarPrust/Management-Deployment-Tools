using DCM.Application.DTOs.OperatingSystem;

namespace DCM.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para gerenciamento de sistemas operacionais.
    /// </summary>
    public interface IOperatingSystemService
    {
        /// <summary>
        /// Obtém todos os sistemas operacionais ativos ordenados por nome.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de sistemas operacionais.</returns>
        Task<IEnumerable<OperatingSystemReadDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém um sistema operacional por ID.
        /// </summary>
        /// <param name="id">ID do sistema operacional.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Sistema operacional encontrado ou null.</returns>
        Task<OperatingSystemReadDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém um sistema operacional por nome curto.
        /// </summary>
        /// <param name="shortName">Nome curto do sistema operacional.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Sistema operacional encontrado ou null.</returns>
        Task<OperatingSystemReadDTO?> GetByShortNameAsync(string shortName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém sistemas operacionais suportados (ativos).
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de sistemas operacionais suportados.</returns>
        Task<IEnumerable<OperatingSystemReadDTO>> GetSupportedVersionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria um novo sistema operacional.
        /// </summary>
        /// <param name="dto">Dados para criação do sistema operacional.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Sistema operacional criado.</returns>
        Task<OperatingSystemReadDTO> CreateAsync(OperatingSystemCreateDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove um sistema operacional (soft delete).
        /// </summary>
        /// <param name="id">ID do sistema operacional a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se removido com sucesso, false se não encontrado.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se um sistema operacional existe.
        /// </summary>
        /// <param name="id">ID do sistema operacional.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se existe, false caso contrário.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
