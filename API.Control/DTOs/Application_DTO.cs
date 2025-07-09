using API.Control.Models;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Control.DTOs
{
    public class Application_DTO
    {
        public Guid Id { get; set; } // opcional no POST

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // opcional no POST, mas obrigatório no PUT e DELETE

        [Required]
        [SwaggerSchema("Nome curto da aplicação")]
        public string NameID { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Nome de exibição da aplicação")]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Versão da aplicação")]
        public string Version { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Nome do arquivo")]
        public string FileName { get; set; } = string.Empty;

        public string Argument { get; set; } = string.Empty;

        [Required]
        public string Source { get; set; } = string.Empty;

        public string Filter { get; set; } = string.Empty;

        public string Hash { get; set; } = string.Empty;

        public bool Enabled { get; set; }


        // Foreign keys for relationships
        public Guid? DeviceModelId { get; set; }

        public Guid? ProfileId { get; set; }

        public Guid? DeviceId { get; set; }

    }
}
