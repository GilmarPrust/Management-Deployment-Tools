using DCM.Core.Entities;
using DCM.Core.Entities.secondary;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface específica para repositório de configurações de aplicações.
    /// </summary>
    public interface IApplicationConfigRepository : IRepository<ApplicationConfig>
    {
        /// <summary>
        /// Obtém uma configuração de aplicação por ID.
        /// </summary>
        /// <param name="id">ID da configuração</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Configuração encontrada ou null</returns>
        Task<ApplicationConfig?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtém aplicações de um grupo específico.
        /// </summary>
        /// <param name="groupId">ID do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de aplicações do grupo</returns>
        Task<IEnumerable<Application>> GetApplicationsByGroupAsync(Guid groupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona uma aplicação a um grupo.
        /// </summary>
        /// <param name="groupId">ID do grupo</param>
        /// <param name="applicationId">ID da aplicação</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task da operação</returns>
        Task AddApplicationToGroupAsync(Guid groupId, Guid applicationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove uma aplicação de um grupo.
        /// </summary>
        /// <param name="groupId">ID do grupo</param>
        /// <param name="applicationId">ID da aplicação</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task da operação</returns>
        Task RemoveApplicationFromGroupAsync(Guid groupId, Guid applicationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um grupo com o nome especificado.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contrário</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria ou obtém o grupo padrão "ALL".
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Grupo padrão "ALL"</returns>
        Task<ApplicationGroup> GetOrCreateDefaultGroupAsync(CancellationToken cancellationToken = default);
    }
}