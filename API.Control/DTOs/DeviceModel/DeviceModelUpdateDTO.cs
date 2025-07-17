using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para atualização de modelo de dispositivo.
    /// </summary>
    public class DeviceModelUpdateDTO
    {
        /// <summary>
        /// Fabricante do dispositivo.
        /// </summary>
        [Required(ErrorMessage = "O campo Manufacturer é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Manufacturer deve ter no máximo 100 caracteres.")]
        public string Manufacturer { get; set; } = string.Empty;

        /// <summary>
        /// Modelo do dispositivo.
        /// </summary>
        [Required(ErrorMessage = "O campo Model é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Model deve ter no máximo 100 caracteres.")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Tipo do dispositivo.
        /// </summary>
        [StringLength(50, ErrorMessage = "O campo Type deve ter no máximo 50 caracteres.")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Indica se está habilitado.
        /// </summary>
        public bool Enabled { get; set; } = true;





        //•	Crie um endpoint específico para associação.
       //•	Use um DTO para receber os IDs dos aplicativos.
        //•	Implemente a lógica no service para adicionar os aplicativos ao modelo.
        //•	Teste pelo Swagger.
        //Se quiser o código completo para todos os arquivos envolvidos, só pedir!*/

    }
}
