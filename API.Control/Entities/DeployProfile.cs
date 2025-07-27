namespace API.Control.Entities
{
    /// <summary>
    /// Representa um perfil de implantação, incluindo imagem, aplicativos e dispositivos associados.
    /// </summary>
    public class DeployProfile : _BaseEntity
    {
        [Required, StringLength(100)]
        public required string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; } = string.Empty;


        public DeployProfile() { }


        [Required]
        public required Guid ImageId { get; set; }
        public virtual Image Image { get; set; } = null!;
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
        public virtual ICollection<ProfileTask> ProfileTasks { get; set; } = new List<ProfileTask>();
    }
}
