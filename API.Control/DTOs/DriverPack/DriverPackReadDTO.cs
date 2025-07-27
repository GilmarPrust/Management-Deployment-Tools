namespace API.Control.DTOs.DriverPack
{
    /// <summary>
    /// DTO para leitura de pacote de driver.
    /// </summary>
    public class DriverPackReadDTO
    {
        public Guid Id { get; init; }
        public string FileName { get; init; } = string.Empty;
        public string OS { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Hash { get; init; } = string.Empty;
        public bool Enabled { get; init; }

    }
}
