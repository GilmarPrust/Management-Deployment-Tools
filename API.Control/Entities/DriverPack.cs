namespace API.Control.Entities
{
    /// <summary>
    /// Representa um pacote de driver associado a um modelo de dispositivo.
    /// </summary>
    public class DriverPack : _BaseEntity
    {

        [Required, StringLength(100)]
        public required string FileName { get; set; }

        [Required, StringLength(50)]
        public required string OS { get; set; }

        [Required, StringLength(50)]
        public required string Version { get; set; }

        [Required, StringLength(200)]
        public required string Source { get; set; }

        [Required, StringLength(64)]
        public required string Hash { get; set; }

        public DriverPack() { }

    }
}
