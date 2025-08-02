namespace API.Control.DTOs.Application
{
    /// <summary>
    /// DTO para leitura de aplicativo.
    /// </summary>
    public class ApplicationReadDTO
    {
        /// <summary>
        /// Identificador único do aplicativo.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Identificador do aplicativo.
        /// </summary>
        public string NameID { get; init; } = string.Empty;

        /// <summary>
        /// Nome de exibição do aplicativo.
        /// </summary>
        public string DisplayName { get; init; } = string.Empty;

        /// <summary>
        /// Versão do aplicativo.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Nome do arquivo de instalação.
        /// </summary>
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Argumentos de linha de comando para instalação.
        /// </summary>
        public string Argument { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do instalador.
        /// </summary>
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Filtro opcional para o aplicativo.
        /// </summary>
        public string Filter { get; init; } = string.Empty;

        /// <summary>
        /// Hash do arquivo de instalação.
        /// </summary>
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o aplicativo está habilitado.
        /// </summary>
        public bool Enabled { get; init; }
    }
}
