using DCM.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using DCM.Core.Entities;

namespace DCM.Core.Entities.secondary
{
    /// <summary>
    /// Representa uma dependência entre aplicações.
    /// </summary>
    public class ApplicationDependency : BaseEntity
    {
        /// <summary>
        /// ID da aplicação que tem a dependência.
        /// </summary>
        [Required]
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// Aplicação que tem a dependência.
        /// </summary>
        public virtual Application Application { get; set; }

        /// <summary>
        /// ID da aplicação da qual depende.
        /// </summary>
        [Required]
        public Guid DependsOnApplicationId { get; set; }

        /// <summary>
        /// Aplicação da qual depende.
        /// </summary>
        public virtual Application DependsOnApplication { get; set; }

        /// <summary>
        /// Descrição da dependência.
        /// </summary>
        [StringLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// Tipo da dependência.
        /// </summary>
        [Required]
        public DependencyType DependencyType { get; private set; } = DependencyType.Required;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public ApplicationDependency() { }

        /// <summary>
        /// Fábrica para criar uma dependência com validações de domínio.
        /// </summary>
        /// <param name="applicationId">Aplicação que tem a dependência</param>
        /// <param name="dependsOnApplicationId">Aplicação da qual depende</param>
        /// <param name="dependencyType">Tipo de dependência</param>
        /// <param name="description">Descrição opcional</param>
        public static ApplicationDependency Create(Guid applicationId, Guid dependsOnApplicationId, DependencyType dependencyType, string? description = null)
        {
            Validate(applicationId, dependsOnApplicationId);

            return new ApplicationDependency
            {
                ApplicationId = applicationId,
                DependsOnApplicationId = dependsOnApplicationId,
                DependencyType = dependencyType,
                Description = description
            };
        }

        /// <summary>
        /// Define/atualiza o tipo de dependência com auditoria.
        /// </summary>
        public void SetDependencyType(DependencyType type)
        {
            if (DependencyType != type)
            {
                DependencyType = type;
                Update();
            }
        }

        /// <summary>
        /// Atualiza a descrição da dependência com auditoria.
        /// </summary>
        public void SetDescription(string? description)
        {
            if (Description != description)
            {
                Description = description;
                Update();
            }
        }

        /// <summary>
        /// Validação de integridade: a aplicação não pode depender de si mesma.
        /// </summary>
        public static void Validate(Guid applicationId, Guid dependsOnApplicationId)
        {
            if (applicationId == Guid.Empty) throw new ArgumentException("ApplicationId cannot be empty", nameof(applicationId));
            if (dependsOnApplicationId == Guid.Empty) throw new ArgumentException("DependsOnApplicationId cannot be empty", nameof(dependsOnApplicationId));
            if (applicationId == dependsOnApplicationId)
                throw new InvalidOperationException("Uma aplicação não pode depender de si mesma.");
        }

        /// <summary>
        /// Helpers semânticos.
        /// </summary>
        public bool IsRequired => DependencyType == DependencyType.Required;
        public bool IsRecommended => DependencyType == DependencyType.Recommended;
        public bool IsOptional => DependencyType == DependencyType.Optional;
        public bool IsConflict => DependencyType == DependencyType.Conflict;
    }
}
