using DCM.Core.Entities.secondary;

namespace DCM.Application.DTOs.ApplicationOrder
{
    /// <summary>
    /// DTO para resultado da ordenação de instalação.
    /// </summary>
    public class InstallationOrderResultDTO
    {
        /// <summary>
        /// Lista ordenada de aplicações para instalação.
        /// </summary>
        public IReadOnlyList<ApplicationInstallationItemDTO> Applications { get; init; } = Array.Empty<ApplicationInstallationItemDTO>();

        /// <summary>
        /// Indica se a ordenação foi bem-sucedida.
        /// </summary>
        public bool IsSuccessful { get; init; }

        /// <summary>
        /// Mensagens de erro ou avisos.
        /// </summary>
        public IReadOnlyList<string> Messages { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Aplicações que não puderam ser incluídas devido a dependências não resolvidas.
        /// </summary>
        public IReadOnlyList<UnresolvedApplicationDTO> UnresolvedApplications { get; init; } = Array.Empty<UnresolvedApplicationDTO>();

        /// <summary>
        /// Tempo estimado total de instalação em minutos.
        /// </summary>
        public int EstimatedTotalTimeMinutes { get; init; }

        /// <summary>
        /// Número total de aplicações a serem instaladas.
        /// </summary>
        public int TotalApplications => Applications.Count;
    }

    /// <summary>
    /// DTO para item de aplicação na ordem de instalação.
    /// </summary>
    public class ApplicationInstallationItemDTO
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
        /// Versão da aplicação.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Ordem de instalação (1 = primeiro).
        /// </summary>
        public int InstallationOrder { get; init; }

        /// <summary>
        /// Prioridade original da aplicação.
        /// </summary>
        public int Priority { get; init; }

        /// <summary>
        /// Indica se é uma instalação obrigatória.
        /// </summary>
        public bool IsRequired { get; init; }

        /// <summary>
        /// Indica se deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; init; }

        /// <summary>
        /// Tempo estimado de instalação em minutos.
        /// </summary>
        public int EstimatedInstallTimeMinutes { get; init; }

        /// <summary>
        /// Grupo de instalação para instalações paralelas.
        /// </summary>
        public string? InstallationGroup { get; init; }

        /// <summary>
        /// Nível de profundidade na árvore de dependências.
        /// </summary>
        public int DependencyLevel { get; init; }

        /// <summary>
        /// IDs das aplicações das quais esta depende diretamente.
        /// </summary>
        public IReadOnlyList<Guid> DirectDependencies { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Arquivos de instalação da aplicação.
        /// </summary>
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Argumentos de instalação.
        /// </summary>
        public string Arguments { get; init; } = string.Empty;
    }

    /// <summary>
    /// DTO para aplicações não resolvidas.
    /// </summary>
    public class UnresolvedApplicationDTO
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
        /// Motivo pelo qual não pôde ser resolvida.
        /// </summary>
        public string Reason { get; init; } = string.Empty;

        /// <summary>
        /// IDs das dependências não encontradas.
        /// </summary>
        public IReadOnlyList<Guid> MissingDependencies { get; init; } = Array.Empty<Guid>();
    }

    /// <summary>
    /// DTO para resultado da validação de dependências.
    /// </summary>
    public class DependencyValidationResultDTO
    {
        /// <summary>
        /// Indica se as dependências são válidas (sem ciclos).
        /// </summary>
        public bool IsValid { get; init; }

        /// <summary>
        /// Dependências circulares detectadas.
        /// </summary>
        public IReadOnlyList<CircularDependencyDTO> CircularDependencies { get; init; } = Array.Empty<CircularDependencyDTO>();

        /// <summary>
        /// Aplicações com dependências não encontradas.
        /// </summary>
        public IReadOnlyList<UnresolvedApplicationDTO> MissingDependencies { get; init; } = Array.Empty<UnresolvedApplicationDTO>();

        /// <summary>
        /// Conflitos de aplicações detectados.
        /// </summary>
        public IReadOnlyList<ApplicationConflictDTO> Conflicts { get; init; } = Array.Empty<ApplicationConflictDTO>();
    }

    /// <summary>
    /// DTO para dependência circular.
    /// </summary>
    public class CircularDependencyDTO
    {
        /// <summary>
        /// Caminho da dependência circular.
        /// </summary>
        public IReadOnlyList<Guid> DependencyPath { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Nomes das aplicações no ciclo.
        /// </summary>
        public IReadOnlyList<string> ApplicationNames { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Descrição do ciclo.
        /// </summary>
        public string Description { get; init; } = string.Empty;
    }

    /// <summary>
    /// DTO para conflito de aplicações.
    /// </summary>
    public class ApplicationConflictDTO
    {
        /// <summary>
        /// ID da primeira aplicação em conflito.
        /// </summary>
        public Guid ApplicationId1 { get; init; }

        /// <summary>
        /// Nome da primeira aplicação.
        /// </summary>
        public string ApplicationName1 { get; init; } = string.Empty;

        /// <summary>
        /// ID da segunda aplicação em conflito.
        /// </summary>
        public Guid ApplicationId2 { get; init; }

        /// <summary>
        /// Nome da segunda aplicação.
        /// </summary>
        public string ApplicationName2 { get; init; } = string.Empty;

        /// <summary>
        /// Descrição do conflito.
        /// </summary>
        public string ConflictDescription { get; init; } = string.Empty;
    }

    /// <summary>
    /// DTO para grupos de instalação paralela.
    /// </summary>
    public class ParallelInstallationGroupsDTO
    {
        /// <summary>
        /// Grupos de aplicações que podem ser instaladas em paralelo.
        /// </summary>
        public IReadOnlyList<ParallelInstallationGroupDTO> Groups { get; init; } = Array.Empty<ParallelInstallationGroupDTO>();

        /// <summary>
        /// Número total de grupos.
        /// </summary>
        public int TotalGroups => Groups.Count;

        /// <summary>
        /// Tempo estimado considerando paralelismo.
        /// </summary>
        public int EstimatedTimeWithParallelism { get; init; }
    }

    /// <summary>
    /// DTO para grupo de instalação paralela.
    /// </summary>
    public class ParallelInstallationGroupDTO
    {
        /// <summary>
        /// Ordem do grupo (1 = primeiro grupo).
        /// </summary>
        public int GroupOrder { get; init; }

        /// <summary>
        /// Nome do grupo.
        /// </summary>
        public string GroupName { get; init; } = string.Empty;

        /// <summary>
        /// Aplicações que podem ser instaladas em paralelo neste grupo.
        /// </summary>
        public IReadOnlyList<ApplicationInstallationItemDTO> Applications { get; init; } = Array.Empty<ApplicationInstallationItemDTO>();

        /// <summary>
        /// Tempo estimado do grupo (aplicação mais demorada).
        /// </summary>
        public int EstimatedGroupTimeMinutes { get; init; }
    }

    /// <summary>
    /// DTO para estimativa de tempo de instalação.
    /// </summary>
    public class InstallationTimeEstimateDTO
    {
        /// <summary>
        /// Tempo estimado total em instalação sequencial.
        /// </summary>
        public int SequentialTimeMinutes { get; init; }

        /// <summary>
        /// Tempo estimado com instalação paralela.
        /// </summary>
        public int ParallelTimeMinutes { get; init; }

        /// <summary>
        /// Economia de tempo com paralelismo.
        /// </summary>
        public int TimeSavedMinutes => SequentialTimeMinutes - ParallelTimeMinutes;

        /// <summary>
        /// Percentual de economia de tempo.
        /// </summary>
        public double TimeSavedPercentage => SequentialTimeMinutes > 0 ? (double)TimeSavedMinutes / SequentialTimeMinutes * 100 : 0;

        /// <summary>
        /// Detalhamento por grupo de instalação.
        /// </summary>
        public IReadOnlyList<GroupTimeEstimateDTO> GroupEstimates { get; init; } = Array.Empty<GroupTimeEstimateDTO>();
    }

    /// <summary>
    /// DTO para estimativa de tempo por grupo.
    /// </summary>
    public class GroupTimeEstimateDTO
    {
        /// <summary>
        /// Ordem do grupo.
        /// </summary>
        public int GroupOrder { get; init; }

        /// <summary>
        /// Nome do grupo.
        /// </summary>
        public string GroupName { get; init; } = string.Empty;

        /// <summary>
        /// Tempo estimado do grupo.
        /// </summary>
        public int EstimatedTimeMinutes { get; init; }

        /// <summary>
        /// Número de aplicações no grupo.
        /// </summary>
        public int ApplicationCount { get; init; }
    }
}