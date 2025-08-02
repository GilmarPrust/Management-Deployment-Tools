namespace API.Control.Entities
{
    /// <summary>
    /// Representa um perfil de implantação, incluindo imagem, aplicativos, dispositivos e tarefas associadas.
    /// </summary>
    public class DeployProfile : BaseEntity
    {
        /// <summary>
        /// Nome do perfil de implantação.
        /// </summary>
        [Required, StringLength(100)]
        public required string Name { get; set; }

        /// <summary>
        /// Descrição do perfil de implantação.
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public DeployProfile() { }

        /// <summary>
        /// ID da imagem associada ao perfil.
        /// </summary>
        [Required]
        public required Guid ImageId { get; set; }

        /// <summary>
        /// Imagem associada ao perfil.
        /// </summary>
        public virtual Image Image { get; set; } = null!;

        /// <summary>
        /// Aplicativos associados ao perfil.
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        /// <summary>
        /// Dispositivos associados ao perfil.
        /// </summary>
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

        /// <summary>
        /// Tarefas de perfil associadas ao perfil de implantação.
        /// </summary>
        public virtual ICollection<ProfileTask> ProfileTasks { get; set; } = new List<ProfileTask>();
    }
}
