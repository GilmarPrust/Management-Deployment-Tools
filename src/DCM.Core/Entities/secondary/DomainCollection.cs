using System;
using System.Collections.Generic;
using System.Linq;

namespace DCM.Core.Entities.secondary
{
    /// <summary>
    /// Coleção de domínio genérica com encapsulamento, validação e callback de mudança.
    /// Pode operar sobre uma coleção interna própria ou sobre uma coleção externa (backing) para suportar EF Core.
    /// </summary>
    /// <typeparam name="T">Tipo do item da coleção</typeparam>
    public class DomainCollection<T> where T : class
    {
        private readonly ICollection<T> _items;
        private readonly Action<T>? _validateAdd;
        private readonly Action<T>? _validateRemove;
        private readonly Action? _onChanged;

        /// <summary>
        /// Cria uma coleção de domínio com lista própria e validações/callback opcionais.
        /// </summary>
        public DomainCollection(Action<T>? validateAdd = null, Action<T>? validateRemove = null, Action? onChanged = null)
            : this(new List<T>(), validateAdd, validateRemove, onChanged)
        { }

        /// <summary>
        /// Cria uma coleção de domínio operando sobre uma coleção externa (backing).
        /// </summary>
        /// <param name="backingCollection">Coleção de suporte que armazenará os itens (útil para EF Core)</param>
        /// <param name="validateAdd">Validação executada antes de adicionar um item</param>
        /// <param name="validateRemove">Validação executada antes de remover um item</param>
        /// <param name="onChanged">Ação executada após uma alteração bem-sucedida (add/remove/clear)</param>
        public DomainCollection(ICollection<T> backingCollection, Action<T>? validateAdd = null, Action<T>? validateRemove = null, Action? onChanged = null)
        {
            _items = backingCollection ?? throw new ArgumentNullException(nameof(backingCollection));
            _validateAdd = validateAdd;
            _validateRemove = validateRemove;
            _onChanged = onChanged;
        }

        /// <summary>
        /// Itens apenas leitura. Se a coleção não suportar IReadOnlyCollection, retorna um snapshot.
        /// </summary>
        public IReadOnlyCollection<T> Items =>
            _items as IReadOnlyCollection<T> ?? new List<T>(_items);

        /// <summary>
        /// Adiciona um item, aplicando validação e garantindo idempotência.
        /// </summary>
        public void Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (_items.Contains(item))
                return;

            _validateAdd?.Invoke(item);

            _items.Add(item);
            _onChanged?.Invoke();
        }

        /// <summary>
        /// Remove um item, aplicando validação e notificando alteração.
        /// </summary>
        public void Remove(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (!_items.Contains(item))
                return;

            _validateRemove?.Invoke(item);

            if (_items.Remove(item))
            {
                _onChanged?.Invoke();
            }
        }

        /// <summary>
        /// Remove todos os itens.
        /// </summary>
        public void Clear()
        {
            if (_items.Count == 0)
                return;

            _items.Clear();
            _onChanged?.Invoke();
        }

        /// <summary>
        /// Verifica se contém o item.
        /// </summary>
        public bool Contains(T item) => item != null && _items.Contains(item);

        /// <summary>
        /// Quantidade de itens.
        /// </summary>
        public int Count => _items.Count;
    }
}

