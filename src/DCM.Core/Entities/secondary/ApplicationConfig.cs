using DCM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DCM.Core.Entities.secondary
{
    /// <summary>
    /// Representa o sistema de prioridades e dependências para instalação de aplicativos.
    /// Controla a ordem de execução baseada em dependências e prioridades.
    /// </summary>
    public class ApplicationConfig : BaseEntity
    {
        /// <summary>
        /// ID da aplicação principal.
        /// </summary>
        [Required]
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// Aplicação principal.
        /// </summary>
        public virtual Application Application { get; set; }

        /// <summary>
        /// Lista de dependências das quais esta aplicação depende (devem executar antes).
        /// </summary>
        public virtual ICollection<ApplicationDependency> Dependencies { get; set; } = new List<ApplicationDependency>();

        /// <summary>
        /// Lista de aplicações que dependem desta (executam depois).
        /// </summary>
        public virtual ICollection<ApplicationDependency> Dependents { get; set; } = new List<ApplicationDependency>();

        /// <summary>
        /// Observações sobre a prioridade ou dependências.
        /// </summary>
        [StringLength(500)]
        public string? Notes { get; set; }

        /// <summary>
        /// Indica se esta aplicação é obrigatória (não pode ser pulada na instalação).
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// Indica se esta aplicação deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; set; } = true;

        /// <summary>
        /// Tempo estimado de instalação em minutos.
        /// </summary>
        [Range(0, 999, ErrorMessage = "O tempo estimado deve estar entre 0 e 999 minutos.")]
        public int EstimatedInstallTimeMinutes { get; set; } = 5;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public ApplicationConfig() { }

        /// <summary>
        /// Construtor para criar uma configuração padrão para uma aplicação.
        /// </summary>
        public ApplicationConfig(Guid applicationId)
        {
            ApplicationId = applicationId;
            IsRequired = false;
            IsSilentInstall = true;
            EstimatedInstallTimeMinutes = 5;
            Notes = "Configuração criada automaticamente";
        }

        /// <summary>
        /// Adiciona uma dependência a esta aplicação.
        /// </summary>
        public void AddDependency(Guid dependencyApplicationId, DependencyType dependencyType = DependencyType.Required)
        {
            if (dependencyApplicationId == ApplicationId)
                throw new InvalidOperationException("Uma aplicação não pode depender de si mesma.");

            var dependency = ApplicationDependency.Create(
                ApplicationId,
                dependencyApplicationId,
                dependencyType
            );

            this.AddItem(Dependencies, dependency);
        }

        /// <summary>
        /// Remove uma dependência desta aplicação.
        /// </summary>
        public void RemoveDependency(Guid dependencyApplicationId)
        {
            var dependency = Dependencies.FirstOrDefault(d => d.DependsOnApplicationId == dependencyApplicationId);
            if (dependency != null)
            {
                this.RemoveItem(Dependencies, dependency);
            }
        }
    }
}
