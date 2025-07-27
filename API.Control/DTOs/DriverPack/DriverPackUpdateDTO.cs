namespace API.Control.DTOs.DriverPack
{
    public class DriverPackUpdateDTO
    {
        [Required]
        public string FileName { get; init; } = string.Empty;

        [Required]
        public string OS { get; init; } = string.Empty;

        [Required]
        public string Version { get; init; } = string.Empty;

        [Required]
        public string Source { get; init; } = string.Empty;

        [Required]
        public string Hash { get; init; } = string.Empty;

        public bool Enabled { get; init; }

    }
}
