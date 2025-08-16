using DCM.Application.DTOs.Application;

namespace DCM.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para gerenciamento de aplicações.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Obtém todas as aplicações ativas.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de aplicações.</returns>
        Task<IEnumerable<ApplicationReadDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém uma aplicação por ID.
        /// </summary>
        /// <param name="id">ID da aplicação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Aplicação encontrada ou null.</returns>
        Task<ApplicationReadDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém uma aplicação por NameID.
        /// </summary>
        /// <param name="nameId">NameID da aplicação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Aplicação encontrada ou null.</returns>
        Task<ApplicationReadDTO?> GetByNameIdAsync(string nameId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém aplicações por versão.
        /// </summary>
        /// <param name="version">Versão das aplicações.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de aplicações com a versão especificada.</returns>
        Task<IEnumerable<ApplicationReadDTO>> GetByVersionAsync(string version, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria uma nova aplicação.
        /// </summary>
        /// <param name="dto">Dados para criação da aplicação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Aplicação criada.</returns>
        Task<ApplicationReadDTO> CreateAsync(ApplicationCreateDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Atualiza uma aplicação existente.
        /// </summary>
        /// <param name="id">ID da aplicação a ser atualizada.</param>
        /// <param name="dto">Dados para atualização da aplicação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Aplicação atualizada ou null se não encontrada.</returns>
        Task<ApplicationReadDTO?> UpdateAsync(Guid id, ApplicationUpdateDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove uma aplicação (soft delete).
        /// </summary>
        /// <param name="id">ID da aplicação a ser removida.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se removida com sucesso, false se não encontrada.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se uma aplicação existe.
        /// </summary>
        /// <param name="id">ID da aplicação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se existe, false caso contrário.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}