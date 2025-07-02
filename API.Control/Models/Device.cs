using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class Device
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nome do computador é obrigatório.")]
        public string ComputerName { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;
        public string MACAddress { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(SerialNumber) && string.IsNullOrWhiteSpace(MACAddress))
            {
                yield return new ValidationResult(
                    "É necessário informar SerialNumber ou MacAddress.",
                    new[] { nameof(SerialNumber), nameof(MACAddress) }
                );
            }
        }

    }
}
