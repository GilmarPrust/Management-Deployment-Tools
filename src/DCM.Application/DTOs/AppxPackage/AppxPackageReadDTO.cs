namespace DCM.Application.DTOs.AppxPackage
{
    /// <summary>
    /// DTO para leitura de pacote Appx.
    /// </summary>
    public class AppxPackageReadDTO
    {
        /// <summary>
        /// Identificador único do pacote.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do pacote.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Versão do pacote.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Publicador do pacote.
        /// </summary>
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
        public string PackageFullName { get; init; } = string.Empty;

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
