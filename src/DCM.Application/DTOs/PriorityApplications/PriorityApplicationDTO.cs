using System.ComponentModel.DataAnnotations;
using DCM.Core.Entities.secondary;

namespace DCM.Application.DTOs.PriorityApplications
{
    /// <summary>
    /// DTO para cria��o de configura��o de prioridade de aplica��o.
    /// </summary>
    public class PriorityApplicationCreateDTO
    {
        /// <summary>
        /// ID da aplica��o.
        /// </summary>
        [Required(ErrorMessage = "O ID da aplica��o � obrigat�rio.")]
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Prioridade de instala��o (1 = maior prioridade).
        /// </summary>
        [Range(1, 9999, ErrorMessage = "A prioridade deve estar entre 1 e 9999.")]
        public int Priority { get; init; } = 100;

        /// <summary>
        /// Lista de IDs de aplica��es das quais esta depende.
        /// </summary>
        public IReadOnlyList<ApplicationDependencyCreateDTO> Dependencies { get; init; } = Array.Empty<ApplicationDependencyCreateDTO>();

        /// <summary>
        /// Observa��es sobre a prioridade.
        /// </summary>
        [StringLength(500, ErrorMessage = "As observa��es devem ter no m�ximo 500 caracteres.")]
        public string? Notes { get; init; }

        /// <summary>
        /// Indica se � obrigat�ria.
        /// </summary>
        public bool IsRequired { get; init; } = false;

        /// <summary>
        /// Indica se deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; init; } = true;

        /// <summary>
        /// Tempo estimado de instala��o em minutos.
        /// </summary>
        [Range(0, 999, ErrorMessage = "O tempo estimado deve estar entre 0 e 999 minutos.")]
        public int EstimatedInstallTimeMinutes { get; init; } = 5;

        /// <summary>
        /// Grupo de instala��o.
        /// </summary>
        [StringLength(50, ErrorMessage = "O grupo de instala��o deve ter no m�ximo 50 caracteres.")]
        public string? InstallationGroup { get; init; }
    }

    /// <summary>
    /// DTO para atualiza��o de configura��o de prioridade de aplica��o.
    /// </summary>
    public class PriorityApplicationUpdateDTO
    {
        /// <summary>
        /// Prioridade de instala��o (1 = maior prioridade).
        /// </summary>
        [Range(1, 9999, ErrorMessage = "A prioridade deve estar entre 1 e 9999.")]
        public int Priority { get; init; } = 100;

        /// <summary>
        /// Lista de depend�ncias (substitui as existentes).
        /// </summary>
        public IReadOnlyList<ApplicationDependencyCreateDTO> Dependencies { get; init; } = Array.Empty<ApplicationDependencyCreateDTO>();

        /// <summary>
        /// Observa��es sobre a prioridade.
        /// </summary>
        [StringLength(500, ErrorMessage = "As observa��es devem ter no m�ximo 500 caracteres.")]
        public string? Notes { get; init; }

        /// <summary>
        /// Indica se � obrigat�ria.
        /// </summary>
        public bool IsRequired { get; init; } = false;

        /// <summary>
        /// Indica se deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; init; } = true;

        /// <summary>
        /// Tempo estimado de instala��o em minutos.
        /// </summary>
        [Range(0, 999, ErrorMessage = "O tempo estimado deve estar entre 0 e 999 minutos.")]
        public int EstimatedInstallTimeMinutes { get; init; } = 5;

        /// <summary>
        /// Grupo de instala��o.
        /// </summary>
        [StringLength(50, ErrorMessage = "O grupo de instala��o deve ter no m�ximo 50 caracteres.")]
        public string? InstallationGroup { get; init; }

        /// <summary>
        /// Indica se est� habilitado.
        /// </summary>
        public bool Enabled { get; init; } = true;
    }

    /// <summary>
    /// DTO para leitura de configura��o de prioridade de aplica��o.
    /// </summary>
    public class PriorityApplicationReadDTO
    {
        /// <summary>
        /// ID da configura��o.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// ID da aplica��o.
        /// </summary>
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Nome da aplica��o.
        /// </summary>
        public string ApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// Vers�o da aplica��o.
        /// </summary>
        public string ApplicationVersion { get; init; } = string.Empty;

        /// <summary>
        /// Prioridade de instala��o.
        /// </summary>
        public int Priority { get; init; }

        /// <summary>
        /// Descri��o do tipo de prioridade.
        /// </summary>
        public string PriorityTypeDescription { get; init; } = string.Empty;

        /// <summary>
        /// Lista de depend�ncias.
        /// </summary>
        public IReadOnlyList<ApplicationDependencyReadDTO> Dependencies { get; init; } = Array.Empty<ApplicationDependencyReadDTO>();

        /// <summary>
        /// Lista de aplica��es dependentes.
        /// </summary>
        public IReadOnlyList<ApplicationDependencyReadDTO> Dependents { get; init; } = Array.Empty<ApplicationDependencyReadDTO>();

        /// <summary>
        /// Observa��es.
        /// </summary>
        public string? Notes { get; init; }

        /// <summary>
        /// Indica se � obrigat�ria.
        /// </summary>
        public bool IsRequired { get; init; }

        /// <summary>
        /// Indica se � instala��o silenciosa.
        /// </summary>
        public bool IsSilentInstall { get; init; }

        /// <summary>
        /// Tempo estimado em minutos.
        /// </summary>
        public int EstimatedInstallTimeMinutes { get; init; }

        /// <summary>
        /// Grupo de instala��o.
        /// </summary>
        public string? InstallationGroup { get; init; }

        /// <summary>
        /// Indica se est� habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Data de cria��o.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Data de atualiza��o.
        /// </summary>
        public DateTime? UpdatedAt { get; init; }
    }

    /// <summary>
    /// DTO para cria��o de depend�ncia de aplica��o.
    /// </summary>
    public class ApplicationDependencyCreateDTO
    {
        /// <summary>
        /// ID da aplica��o da qual depende.
        /// </summary>
        [Required(ErrorMessage = "O ID da aplica��o depend�ncia � obrigat�rio.")]
        public Guid DependsOnApplicationId { get; init; }

        /// <summary>
        /// Descri��o da depend�ncia.
        /// </summary>
        [StringLength(200, ErrorMessage = "A descri��o deve ter no m�ximo 200 caracteres.")]
        public string? Description { get; init; }
    }

    /// <summary>
    /// DTO para leitura de depend�ncia de aplica��o.
    /// </summary>
    public class ApplicationDependencyReadDTO
    {
        /// <summary>
        /// ID da depend�ncia.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// ID da aplica��o que tem a depend�ncia.
        /// </summary>
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Nome da aplica��o que tem a depend�ncia.
        /// </summary>
        public string ApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// ID da aplica��o da qual depende.
        /// </summary>
        public Guid DependsOnApplicationId { get; init; }

        /// <summary>
        /// Nome da aplica��o da qual depende.
        /// </summary>
        public string DependsOnApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// Vers�o da aplica��o da qual depende.
        /// </summary>
        public string DependsOnApplicationVersion { get; init; } = string.Empty;

        /// <summary>
        /// Descri��o do tipo de depend�ncia.
        /// </summary>
        public string DependencyTypeDescription { get; init; } = string.Empty;

        /// <summary>
        /// Descri��o da depend�ncia.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Indica se est� habilitada.
        /// </summary>
        public bool Enabled { get; init; }
    }

    /// <summary>
    /// DTO para configura��o em lote de prioridades.
    /// </summary>
    public class BatchPriorityConfigurationDTO
    {
        /// <summary>
        /// Configura��es de prioridade a serem aplicadas.
        /// </summary>
        public IReadOnlyList<PriorityApplicationCreateDTO> Configurations { get; init; } = Array.Empty<PriorityApplicationCreateDTO>();

        /// <summary>
        /// Indica se deve sobrescrever configura��es existentes.
        /// </summary>
        public bool OverwriteExisting { get; init; } = false;

        /// <summary>
        /// Indica se deve validar depend�ncias circulares.
        /// </summary>
        public bool ValidateCircularDependencies { get; init; } = true;
    }

    /// <summary>
    /// DTO para resultado de configura��o em lote.
    /// </summary>
    public class BatchPriorityConfigurationResultDTO
    {
        /// <summary>
        /// Indica se a opera��o foi bem-sucedida.
        /// </summary>
        public bool IsSuccessful { get; init; }

        /// <summary>
        /// Configura��es criadas com sucesso.
        /// </summary>
        public IReadOnlyList<PriorityApplicationReadDTO> CreatedConfigurations { get; init; } = Array.Empty<PriorityApplicationReadDTO>();

        /// <summary>
        /// Configura��es que falharam.
        /// </summary>
        public IReadOnlyList<FailedConfigurationDTO> FailedConfigurations { get; init; } = Array.Empty<FailedConfigurationDTO>();

        /// <summary>
        /// Mensagens de resultado.
        /// </summary>
        public IReadOnlyList<string> Messages { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Depend�ncias circulares detectadas (se houver).
        /// </summary>
        public IReadOnlyList<string> CircularDependencies { get; init; } = Array.Empty<string>();
    }

    /// <summary>
    /// DTO para configura��o que falhou.
    /// </summary>
    public class FailedConfigurationDTO
    {
        /// <summary>
        /// ID da aplica��o.
        /// </summary>
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Nome da aplica��o.
        /// </summary>
        public string ApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// Motivo da falha.
        /// </summary>
        public string Reason { get; init; } = string.Empty;

        /// <summary>
        /// Detalhes do erro.
        /// </summary>
        public string? ErrorDetails { get; init; }
    }
}