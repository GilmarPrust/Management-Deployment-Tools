using System.ComponentModel.DataAnnotations;
using DCM.Core.Entities.secondary;

namespace DCM.Application.DTOs.PriorityApplications
{
    /// <summary>
    /// DTO para criação de configuração de prioridade de aplicação.
    /// </summary>
    public class PriorityApplicationCreateDTO
    {
        /// <summary>
        /// ID da aplicação.
        /// </summary>
        [Required(ErrorMessage = "O ID da aplicação é obrigatório.")]
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Prioridade de instalação (1 = maior prioridade).
        /// </summary>
        [Range(1, 9999, ErrorMessage = "A prioridade deve estar entre 1 e 9999.")]
        public int Priority { get; init; } = 100;

        /// <summary>
        /// Lista de IDs de aplicações das quais esta depende.
        /// </summary>
        public IReadOnlyList<ApplicationDependencyCreateDTO> Dependencies { get; init; } = Array.Empty<ApplicationDependencyCreateDTO>();

        /// <summary>
        /// Observações sobre a prioridade.
        /// </summary>
        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres.")]
        public string? Notes { get; init; }

        /// <summary>
        /// Indica se é obrigatória.
        /// </summary>
        public bool IsRequired { get; init; } = false;

        /// <summary>
        /// Indica se deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; init; } = true;

        /// <summary>
        /// Tempo estimado de instalação em minutos.
        /// </summary>
        [Range(0, 999, ErrorMessage = "O tempo estimado deve estar entre 0 e 999 minutos.")]
        public int EstimatedInstallTimeMinutes { get; init; } = 5;

        /// <summary>
        /// Grupo de instalação.
        /// </summary>
        [StringLength(50, ErrorMessage = "O grupo de instalação deve ter no máximo 50 caracteres.")]
        public string? InstallationGroup { get; init; }
    }

    /// <summary>
    /// DTO para atualização de configuração de prioridade de aplicação.
    /// </summary>
    public class PriorityApplicationUpdateDTO
    {
        /// <summary>
        /// Prioridade de instalação (1 = maior prioridade).
        /// </summary>
        [Range(1, 9999, ErrorMessage = "A prioridade deve estar entre 1 e 9999.")]
        public int Priority { get; init; } = 100;

        /// <summary>
        /// Lista de dependências (substitui as existentes).
        /// </summary>
        public IReadOnlyList<ApplicationDependencyCreateDTO> Dependencies { get; init; } = Array.Empty<ApplicationDependencyCreateDTO>();

        /// <summary>
        /// Observações sobre a prioridade.
        /// </summary>
        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres.")]
        public string? Notes { get; init; }

        /// <summary>
        /// Indica se é obrigatória.
        /// </summary>
        public bool IsRequired { get; init; } = false;

        /// <summary>
        /// Indica se deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; init; } = true;

        /// <summary>
        /// Tempo estimado de instalação em minutos.
        /// </summary>
        [Range(0, 999, ErrorMessage = "O tempo estimado deve estar entre 0 e 999 minutos.")]
        public int EstimatedInstallTimeMinutes { get; init; } = 5;

        /// <summary>
        /// Grupo de instalação.
        /// </summary>
        [StringLength(50, ErrorMessage = "O grupo de instalação deve ter no máximo 50 caracteres.")]
        public string? InstallationGroup { get; init; }

        /// <summary>
        /// Indica se está habilitado.
        /// </summary>
        public bool Enabled { get; init; } = true;
    }

    /// <summary>
    /// DTO para leitura de configuração de prioridade de aplicação.
    /// </summary>
    public class PriorityApplicationReadDTO
    {
        /// <summary>
        /// ID da configuração.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// ID da aplicação.
        /// </summary>
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public string ApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// Versão da aplicação.
        /// </summary>
        public string ApplicationVersion { get; init; } = string.Empty;

        /// <summary>
        /// Prioridade de instalação.
        /// </summary>
        public int Priority { get; init; }

        /// <summary>
        /// Descrição do tipo de prioridade.
        /// </summary>
        public string PriorityTypeDescription { get; init; } = string.Empty;

        /// <summary>
        /// Lista de dependências.
        /// </summary>
        public IReadOnlyList<ApplicationDependencyReadDTO> Dependencies { get; init; } = Array.Empty<ApplicationDependencyReadDTO>();

        /// <summary>
        /// Lista de aplicações dependentes.
        /// </summary>
        public IReadOnlyList<ApplicationDependencyReadDTO> Dependents { get; init; } = Array.Empty<ApplicationDependencyReadDTO>();

        /// <summary>
        /// Observações.
        /// </summary>
        public string? Notes { get; init; }

        /// <summary>
        /// Indica se é obrigatória.
        /// </summary>
        public bool IsRequired { get; init; }

        /// <summary>
        /// Indica se é instalação silenciosa.
        /// </summary>
        public bool IsSilentInstall { get; init; }

        /// <summary>
        /// Tempo estimado em minutos.
        /// </summary>
        public int EstimatedInstallTimeMinutes { get; init; }

        /// <summary>
        /// Grupo de instalação.
        /// </summary>
        public string? InstallationGroup { get; init; }

        /// <summary>
        /// Indica se está habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Data de criação.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Data de atualização.
        /// </summary>
        public DateTime? UpdatedAt { get; init; }
    }

    /// <summary>
    /// DTO para criação de dependência de aplicação.
    /// </summary>
    public class ApplicationDependencyCreateDTO
    {
        /// <summary>
        /// ID da aplicação da qual depende.
        /// </summary>
        [Required(ErrorMessage = "O ID da aplicação dependência é obrigatório.")]
        public Guid DependsOnApplicationId { get; init; }

        /// <summary>
        /// Descrição da dependência.
        /// </summary>
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string? Description { get; init; }
    }

    /// <summary>
    /// DTO para leitura de dependência de aplicação.
    /// </summary>
    public class ApplicationDependencyReadDTO
    {
        /// <summary>
        /// ID da dependência.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// ID da aplicação que tem a dependência.
        /// </summary>
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Nome da aplicação que tem a dependência.
        /// </summary>
        public string ApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// ID da aplicação da qual depende.
        /// </summary>
        public Guid DependsOnApplicationId { get; init; }

        /// <summary>
        /// Nome da aplicação da qual depende.
        /// </summary>
        public string DependsOnApplicationName { get; init; } = string.Empty;

        /// <summary>
        /// Versão da aplicação da qual depende.
        /// </summary>
        public string DependsOnApplicationVersion { get; init; } = string.Empty;

        /// <summary>
        /// Descrição do tipo de dependência.
        /// </summary>
        public string DependencyTypeDescription { get; init; } = string.Empty;

        /// <summary>
        /// Descrição da dependência.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Indica se está habilitada.
        /// </summary>
        public bool Enabled { get; init; }
    }

    /// <summary>
    /// DTO para configuração em lote de prioridades.
    /// </summary>
    public class BatchPriorityConfigurationDTO
    {
        /// <summary>
        /// Configurações de prioridade a serem aplicadas.
        /// </summary>
        public IReadOnlyList<PriorityApplicationCreateDTO> Configurations { get; init; } = Array.Empty<PriorityApplicationCreateDTO>();

        /// <summary>
        /// Indica se deve sobrescrever configurações existentes.
        /// </summary>
        public bool OverwriteExisting { get; init; } = false;

        /// <summary>
        /// Indica se deve validar dependências circulares.
        /// </summary>
        public bool ValidateCircularDependencies { get; init; } = true;
    }

    /// <summary>
    /// DTO para resultado de configuração em lote.
    /// </summary>
    public class BatchPriorityConfigurationResultDTO
    {
        /// <summary>
        /// Indica se a operação foi bem-sucedida.
        /// </summary>
        public bool IsSuccessful { get; init; }

        /// <summary>
        /// Configurações criadas com sucesso.
        /// </summary>
        public IReadOnlyList<PriorityApplicationReadDTO> CreatedConfigurations { get; init; } = Array.Empty<PriorityApplicationReadDTO>();

        /// <summary>
        /// Configurações que falharam.
        /// </summary>
        public IReadOnlyList<FailedConfigurationDTO> FailedConfigurations { get; init; } = Array.Empty<FailedConfigurationDTO>();

        /// <summary>
        /// Mensagens de resultado.
        /// </summary>
        public IReadOnlyList<string> Messages { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Dependências circulares detectadas (se houver).
        /// </summary>
        public IReadOnlyList<string> CircularDependencies { get; init; } = Array.Empty<string>();
    }

    /// <summary>
    /// DTO para configuração que falhou.
    /// </summary>
    public class FailedConfigurationDTO
    {
        /// <summary>
        /// ID da aplicação.
        /// </summary>
        public Guid ApplicationId { get; init; }

        /// <summary>
        /// Nome da aplicação.
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