using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DCM.Core.Entities;
using DCM.Core.Interfaces.Repositories;
using DCM.Infrastructure.Persistence;

namespace DCM.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação base do padrão Repository para Entity Framework.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade que herda de BaseEntity</typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Inicializa uma nova instância do repositório.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        /// <inheritdoc/>
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => !e.DeletedAt.HasValue) // Filtro para soft delete
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => !e.DeletedAt.HasValue) // Filtro para soft delete
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => !e.DeletedAt.HasValue) // Filtro para soft delete
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => !e.DeletedAt.HasValue) // Filtro para soft delete
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => !e.DeletedAt.HasValue) // Filtro para soft delete
                .AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(e => !e.DeletedAt.HasValue); // Filtro para soft delete
            
            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual T Add(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            // Não é necessário definir CreatedAt e Enabled aqui, 
            // pois o construtor da BaseEntity já os define
            // entity.SetCreatedAt(DateTime.UtcNow); // Opcional, se quiser forçar um novo timestamp
            // entity.Enabled = true; // Já definido no construtor

            return _dbSet.Add(entity).Entity;
        }

        /// <inheritdoc/>
        public virtual void AddRange(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            var entitiesList = entities.ToList();
            // var now = DateTime.UtcNow;

            // foreach (var entity in entitiesList)
            // {
            //     entity.SetCreatedAt(now); // Opcional
            //     entity.Enabled = true; // Já definido no construtor
            // }

            _dbSet.AddRange(entitiesList);
        }

        /// <inheritdoc/>
        public virtual T Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.Update();

            _dbSet.Update(entity);
            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return false;

            Delete(entity);
            return true;
        }

        /// <inheritdoc/>
        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Soft delete
            entity.SoftDelete();

            _dbSet.Update(entity);
        }

        /// <inheritdoc/>
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            var entitiesList = entities.ToList();

            foreach (var entity in entitiesList)
            {
                entity.SoftDelete();
            }

            _dbSet.UpdateRange(entitiesList);
        }

        /// <inheritdoc/>
        public virtual IQueryable<T> GetWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.Where(e => !e.DeletedAt.HasValue).AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}