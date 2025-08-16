using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DCM.Core.Entities.secondary
{
    /// <summary>
    /// Representa um grupo de aplica��es para organiza��o e implanta��o conjunta.
    /// </summary>
    public class ApplicationGroup : BaseEntity
    {
        /// <summary>
        /// Nome do grupo de aplica��es.
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descri��o do grupo de aplica��es.
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Ordem de prioridade do grupo (menor n�mero = maior prioridade).
        /// </summary>
        public int Priority { get; set; } = 0;

        /// <summary>
        /// Categoria do grupo (ex: "Produtividade", "Desenvolvimento", "Sistema").
        /// </summary>
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public ApplicationGroup() { }

        /// <summary>
        /// Aplica��es associadas ao grupo (relacionamento many-to-many).
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        /// <summary>
        /// Perfis de implanta��o que utilizam este grupo.
        /// </summary>
        public virtual ICollection<DeployProfile> DeployProfiles { get; set; } = new List<DeployProfile>();
    }
}