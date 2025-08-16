using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DCM.Core.Entities;

namespace DCM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface gen�rica para reposit�rios, fornecendo opera��es CRUD b�sicas.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade que herda de BaseEntity</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Obt�m uma entidade por ID.
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Entidade encontrada ou null</returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m todas as entidades.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de entidades</returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m entidades com base em um predicado.
        /// </summary>
        /// <param name="predicate">Condi��o de filtro</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Lista de entidades filtradas</returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obt�m a primeira entidade que atende ao predicado.
        /// </summary>
        /// <param name="predicate">Condi��o de filtro</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Primeira entidade encontrada ou null</returns>
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica se existe alguma entidade que atende ao predicado.
        /// </summary>
        /// <param name="predicate">Condi��o de verifica��o</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>True se existe, false caso contr�rio</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Conta as entidades que atendem ao predicado.
        /// </summary>
        /// <param name="predicate">Condi��o de contagem</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>N�mero de entidades</returns>
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona uma nova entidade.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        /// <returns>Entidade adicionada</returns>
        T Add(T entity);

        /// <summary>
        /// Adiciona m�ltiplas entidades.
        /// </summary>
        /// <param name="entities">Entidades a serem adicionadas</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Atualiza uma entidade existente.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        /// <returns>Entidade atualizada</returns>
        T Update(T entity);

        /// <summary>
        /// Remove uma entidade por ID.
        /// </summary>
        /// <param name="id">ID da entidade a ser removida</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove uma entidade.
        /// </summary>
        /// <param name="entity">Entidade a ser removida</param>
        void Delete(T entity);

        /// <summary>
        /// Remove m�ltiplas entidades.
        /// </summary>
        /// <param name="entities">Entidades a serem removidas</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Obt�m entidades com includes especificados.
        /// </summary>
        /// <param name="includes">Propriedades a serem inclu�das</param>
        /// <returns>Queryable com includes</returns>
        IQueryable<T> GetWithIncludes(params Expression<Func<T, object>>[] includes);
    }
}