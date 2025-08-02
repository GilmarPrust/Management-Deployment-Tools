namespace API.Control.Entities
{
    /// <summary>
    /// Representa um pacote Appx associado a dispositivos.
    /// </summary>
    public class AppxPackage : BaseEntity
    {
        /// <summary>
        /// Nome do pacote Appx.
        /// </summary>
        [Required, StringLength(100)]
        public required string Name { get; set; }

        /// <summary>
        /// Versão do pacote Appx.
        /// </summary>
        [Required, StringLength(50)]
        public required string Version { get; set; }

        /// <summary>
        /// Publicador do pacote Appx.
        /// </summary>
        [Required, StringLength(100)]
        public required string Publisher { get; set; }

        /// <summary>
        /// Arquitetura do pacote Appx.
        /// </summary>
        [StringLength(50)]
        public string Architecture { get; set; } = string.Empty;

        /// <summary>
        /// Nome da família do pacote Appx.
        /// </summary>
        [StringLength(100)]
        public string PackageFamilyName { get; set; } = string.Empty;

        /// <summary>
        /// Nome completo do pacote Appx.
        /// </summary>
        [Required, StringLength(200)]
        public required string PackageFullName { get; set; }

        /// <summary>
        /// Indica se o pacote é um framework.
        /// </summary>
        public bool IsFramework { get; set; } = false;

        /// <summary>
        /// Indica se o pacote é um bundle.
        /// </summary>
        public bool IsBundle { get; set; } = false;

        /// <summary>
        /// Indica se o pacote é um pacote de recursos.
        /// </summary>
        public bool IsResourcePackage { get; set; } = false;

        /// <summary>
        /// Indica se o pacote está em modo de desenvolvimento.
        /// </summary>
        public bool IsDevelopmentMode { get; set; } = false;

        /// <summary>
        /// Indica se o pacote está parcialmente instalado.
        /// </summary>
        public bool IsPartiallyStaged { get; set; } = false;

        /// <summary>
        /// Status do pacote Appx.
        /// </summary>
        [StringLength(100)]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public AppxPackage() { }

        /// <summary>
        /// Dispositivos associados ao pacote Appx.
        /// </summary>
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}

// implementar Grupos Appx.