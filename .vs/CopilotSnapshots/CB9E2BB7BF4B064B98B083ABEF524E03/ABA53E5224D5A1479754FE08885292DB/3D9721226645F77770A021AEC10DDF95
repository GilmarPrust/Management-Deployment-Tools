using DCM.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCM.Core.Services.Interfaces
{
    /// <summary>
    /// Interface genérica para serviços de atribuição/associação entre entidades do domínio.
    /// Fornece operações básicas CRUD para gerenciar associações de qualquer tipo de entidade.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade que será gerenciada pelo serviço de atribuição. 
    /// Deve ser uma classe de referência (class constraint).</typeparam>
    /// <remarks>
    /// Esta interface é projetada para ser utilizada em cenários onde é necessário
    /// gerenciar associações ou atribuições entre entidades do domínio, como:
    /// - Atribuição de dispositivos a perfis de implantação
    /// - Associação de aplicações a grupos
    /// - Vinculação de usuários a papéis
    /// 
    /// A interface utiliza o padrão Repository através da injeção de IRepository,
    /// seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD).
    /// </remarks>
    /// <example>
    /// Exemplo de implementação para atribuição de dispositivos:
    /// <code>
    /// public class DeviceAssignmentService : IAssignmentService&lt;Device&gt;
    /// {
    ///     private readonly IRepository&lt;Device&gt; _repository;
    ///     
    ///     public DeviceAssignmentService(IRepository&lt;Device&gt; repository)
    ///     {
    ///         _repository = repository;
    ///     }
    ///     
    ///     // Implementação dos métodos...
    /// }
    /// </code>
    /// </example>
    internal interface IAssignmentService<T> where T : class
    {
        /// <summary>
        /// Obtém todas as entidades do tipo T disponíveis no sistema.
        /// </summary>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. 
        /// O resultado da tarefa contém uma coleção enumerável de todas as entidades do tipo T.
        /// </returns>
        /// <remarks>
        /// Este método recupera todas as entidades sem aplicar filtros.
        /// Para cenários com grande volume de dados, considere implementar paginação
        /// ou filtros específicos em métodos especializados.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando ocorre um erro durante a operação de busca no repositório.
        /// </exception>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Obtém uma entidade específica pelo seu identificador único.
        /// </summary>
        /// <param name="id">
        /// O identificador único (GUID) da entidade a ser recuperada.
        /// Não pode ser Guid.Empty ou null.
        /// </param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona.
        /// O resultado da tarefa contém a entidade encontrada ou null se não existir.
        /// </returns>
        /// <remarks>
        /// Este método realiza uma busca pela chave primária da entidade.
        /// Retorna null se a entidade não for encontrada, permitindo ao chamador
        /// decidir como tratar a ausência do registro.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Lançada quando o parâmetro id é Guid.Empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando ocorre um erro durante a operação de busca no repositório.
        /// </exception>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adiciona uma nova entidade ao sistema de forma assíncrona.
        /// </summary>
        /// <param name="entity">
        /// A entidade do tipo T a ser adicionada. Não pode ser null.
        /// A entidade deve estar em um estado válido para persistência.
        /// </param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona de adição.
        /// A operação é concluída quando a entidade é persistida com sucesso.
        /// </returns>
        /// <remarks>
        /// Este método adiciona a entidade ao contexto e persiste as mudanças.
        /// A entidade receberá um novo ID único após a persistência bem-sucedida.
        /// Validações de domínio devem ser realizadas antes da chamada deste método.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando o parâmetro entity é null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando a entidade já existe no contexto ou quando ocorre
        /// erro durante a operação de persistência.
        /// </exception>
        Task AddAsync(T entity);

        /// <summary>
        /// Atualiza uma entidade existente no sistema de forma assíncrona.
        /// </summary>
        /// <param name="entity">
        /// A entidade do tipo T com as alterações a serem persistidas. Não pode ser null.
        /// A entidade deve possuir um ID válido que corresponda a um registro existente.
        /// </param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona de atualização.
        /// A operação é concluída quando as alterações são persistidas com sucesso.
        /// </returns>
        /// <remarks>
        /// Este método atualiza os dados de uma entidade existente.
        /// A entidade deve ter sido previamente recuperada do repositório ou
        /// deve possuir um ID válido que corresponda a um registro existente.
        /// Apenas as propriedades modificadas serão atualizadas no banco de dados.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando o parâmetro entity é null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando a entidade não é encontrada para atualização ou
        /// quando ocorre erro durante a operação de persistência.
        /// </exception>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Remove uma entidade do sistema pelo seu identificador único.
        /// </summary>
        /// <param name="id">
        /// O identificador único (GUID) da entidade a ser removida.
        /// Não pode ser Guid.Empty.
        /// </param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona de remoção.
        /// A operação é concluída quando a entidade é removida com sucesso.
        /// </returns>
        /// <remarks>
        /// Este método realiza a remoção física ou lógica da entidade, dependendo
        /// da implementação do repositório subjacente. Para entidades que implementam
        /// soft delete, a entidade será marcada como removida mas não será fisicamente
        /// deletada do banco de dados.
        /// 
        /// Se a entidade não existir, a operação será tratada como bem-sucedida
        /// (comportamento idempotente).
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Lançada quando o parâmetro id é Guid.Empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando ocorre erro durante a operação de remoção,
        /// como violações de integridade referencial.
        /// </exception>
        Task DeleteAsync(Guid id);
    }
}
