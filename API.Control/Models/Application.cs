using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um aplicativo associado a dispositivos e modelos.
    /// </summary>
    public class Application
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required, StringLength(50)]
        public required string NameID { get; set; }

        [Required, StringLength(100)]
        public required string DisplayName { get; set; }

        [Required, StringLength(50)]
        public required string Version { get; set; }

        [Required, StringLength(100)]
        public required string FileName { get; set; }

        [StringLength(250)]
        public string Argument { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public required string Source { get; set; }

        [StringLength(100)]
        public string Filter { get; set; } = string.Empty;

        [StringLength(64)]
        public string Hash { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public Application() { }

        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
        public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();
        public virtual ICollection<DeployProfile> ProfileDeploys { get; set; } = new List<DeployProfile>();
    }
}
