using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DriverPack
    {
        [Required(ErrorMessage = "  ")]
        public string DeviceModelGuid { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome do arquivo é obrigatório.")]
        public string FileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome do arquivo é obrigatório.")]
        public string OS { get; set; } = string.Empty;

        [Required(ErrorMessage = "Versão é obrigatória.")]
        public string Version { get; set; } = string.Empty;

        [Required(ErrorMessage = "Caminho é obrigatório.")]
        public string Source { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hash é obrigatório.")]
        public string Hash { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

    }
}
