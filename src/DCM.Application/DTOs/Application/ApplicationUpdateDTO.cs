using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.Application
{
    /// <summary>
    /// DTO para atualização de aplicativo.
    /// </summary>
    public class ApplicationUpdateDTO
    {
        /// <summary>
        /// Identificador do aplicativo.
        /// </summary>
        [Required, StringLength(50)]
        public string NameID { get; init; } = string.Empty;

        /// <summary>
        /// Nome de exibição do aplicativo.
        /// </summary>
        [Required, StringLength(100)]
        public string DisplayName { get; init; } = string.Empty;

        /// <summary>
        /// Versão do aplicativo.
        /// </summary>
        [Required, StringLength(50)]
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Nome do arquivo de instalação.
        /// </summary>
        [Required, StringLength(100)]
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Argumentos de linha de comando para instalação.
        /// </summary>
        [StringLength(250)]
        public string Argument { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do instalador.
        /// </summary>
        [Required, StringLength(200)]
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Filtro opcional para o aplicativo.
        /// </summary>
        [StringLength(100)]
        public string Filter { get; init; } = string.Empty;

        /// <summary>
        /// Hash do arquivo de instalação.
        /// </summary>
        [Required, StringLength(64)]
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o aplicativo está habilitado.
        /// </summary>
        public bool Enabled { get; init; }
    }
}
