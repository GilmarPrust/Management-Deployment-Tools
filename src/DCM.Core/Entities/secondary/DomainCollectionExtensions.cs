using System;
using System.Collections.Generic;
using DCM.Core.Entities;

namespace DCM.Core.Entities.secondary
{
    /// <summary>
    /// Extensões utilitárias para manipular coleções de domínio com validação e auditoria.
    /// </summary>
    public static class DomainCollectionExtensions
    {
        /// <summary>
        /// Adiciona um item à coleção de navegação com validação e marca a entidade como alterada.
        /// </summary>
        public static void AddItem<T>(this BaseEntity owner, ICollection<T> backing, T item, Action<T>? validate = null)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(owner);
            ArgumentNullException.ThrowIfNull(backing);

            var collection = new DomainCollection<T>(backing, validateAdd: validate, onChanged: owner.Update);
            collection.Add(item);
        }

        /// <summary>
        /// Remove um item da coleção de navegação com validação opcional e marca a entidade como alterada.
        /// </summary>
        public static void RemoveItem<T>(this BaseEntity owner, ICollection<T> backing, T item, Action<T>? validateRemove = null)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(owner);
            ArgumentNullException.ThrowIfNull(backing);

            var collection = new DomainCollection<T>(backing, validateRemove: validateRemove, onChanged: owner.Update);
            collection.Remove(item);
        }

        /// <summary>
        /// Limpa a coleção e marca a entidade como alterada.
        /// </summary>
        public static void ClearItems<T>(this BaseEntity owner, ICollection<T> backing)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(owner);
            ArgumentNullException.ThrowIfNull(backing);

            var collection = new DomainCollection<T>(backing, onChanged: owner.Update);
            collection.Clear();
        }

        /// <summary>
        /// Verifica se a coleção contém o item (null-safe).
        /// </summary>
        public static bool ContainsItem<T>(this ICollection<T> backing, T item)
            where T : class
        {
            return item != null && backing.Contains(item);
        }
    }
}
