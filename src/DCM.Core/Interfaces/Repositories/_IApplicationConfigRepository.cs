using DCM.Core.Entities;
using DCM.Core.Entities.secondary;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface espec�fica para reposit�rio de configura��es de aplica��es.
    /// </summary>
    public interface IApplicationConfigRepository : IRepository<ApplicationConfig>
    {
        /// <summary>
        /// Obt�m uma configura��o de aplica��o por ID.
        /// </summary>
        /// <param name="id">ID da configura��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Configura��o encontrada ou null</returns>
        Task<ApplicationConfig?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m aplica��es de um grupo espec�fico.
        /// </summary>
        /// <param name="groupId">ID do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de aplica��es do grupo</returns>
        Task<IEnumerable<Application>> GetApplicationsByGroupAsync(Guid groupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona uma aplica��o a um grupo.
        /// </summary>
        /// <param name="groupId">ID do grupo</param>
        /// <param name="applicationId">ID da aplica��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task da opera��o</returns>
        Task AddApplicationToGroupAsync(Guid groupId, Guid applicationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove uma aplica��o de um grupo.
        /// </summary>
        /// <param name="groupId">ID do grupo</param>
        /// <param name="applicationId">ID da aplica��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Task da opera��o</returns>
        Task RemoveApplicationFromGroupAsync(Guid groupId, Guid applicationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe um grupo com o nome especificado.
        /// </summary>
        /// <param name="name">Nome do grupo</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cria ou obt�m o grupo padr�o "ALL".
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Grupo padr�o "ALL"</returns>
        Task<ApplicationGroup> GetOrCreateDefaultGroupAsync(CancellationToken cancellationToken = default);
    }
}