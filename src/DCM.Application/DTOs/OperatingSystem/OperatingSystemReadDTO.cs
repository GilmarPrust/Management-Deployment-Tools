namespace DCM.Application.DTOs.OperatingSystem
{
    /// <summary>
    /// DTO para leitura de sistema operacional.
    /// </summary>
    public class OperatingSystemReadDTO
    {
        /// <summary>
        /// Identificador único do sistema operacional.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do sistema operacional.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Nome curto do sistema operacional.
        /// </summary>
        public string ShortName { get; init; } = string.Empty;
    }
}
