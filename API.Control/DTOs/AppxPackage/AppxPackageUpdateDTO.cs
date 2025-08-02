namespace API.Control.DTOs.AppxPackage
{
    /// <summary>
    /// DTO para atualização de pacote Appx.
    /// </summary>
    public class AppxPackageUpdateDTO
    {
        /// <summary>
        /// Nome do pacote.
        /// </summary>
        [Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Versão do pacote.
        /// </summary>
        [Required]
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Publicador do pacote.
        /// </summary>
        [Required]
        public string Publisher { get; init; } = string.Empty;

        /// <summary>
        /// Arquitetura do pacote.
        /// </summary>
        public string Architecture { get; init; } = string.Empty;

        /// <summary>
        /// Nome da família do pacote.
        /// </summary>
        public string PackageFamilyName { get; init; } = string.Empty;

        /// <summary>
        /// Nome completo do pacote.
        /// </summary>
        [Required]
        public string PackageFullName { get; init; } = string.Empty;

        /// <summary>
        /// Indica se é um framework.
        /// </summary>
        public bool IsFramework { get; init; }

        /// <summary>
        /// Indica se é um bundle.
        /// </summary>
        public bool IsBundle { get; init; }

        /// <summary>
        /// Indica se é um pacote de recursos.
        /// </summary>
        public bool IsResourcePackage { get; init; }

        /// <summary>
        /// Indica se está em modo de desenvolvimento.
        /// </summary>
        public bool IsDevelopmentMode { get; init; }

        /// <summary>
        /// Indica se está parcialmente instalado.
        /// </summary>
        public bool IsPartiallyStaged { get; init; }

        /// <summary>
        /// Status do pacote.
        /// </summary>
        public string Status { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o pacote está habilitado.
        /// </summary>
        public bool Enabled { get; init; }
    }
}
