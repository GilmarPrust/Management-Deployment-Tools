namespace DCM.Application.DTOs.Manufacturer
{
    /// <summary>
    /// DTO para leitura de fabricante.
    /// </summary>
    public class ManufacturerReadDTO
    {
        /// <summary>
        /// Identificador único do fabricante.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome completo do fabricante.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Nome curto do fabricante.
        /// </summary>
        public string ShortName { get; init; } = string.Empty;
    }
}
