using DCM.Core.Entities.secondary;

namespace DCM.Application.DTOs.ApplicationOrder
{
    /// <summary>
    /// DTO para resultado da ordena��o de instala��o.
    /// </summary>
    public class InstallationOrderResultDTO
    {
        /// <summary>
        /// Lista ordenada de aplica��es para instala��o.
        /// </summary>
        public IReadOnlyList<ApplicationInstallationItemDTO> Applications { get; init; } = Array.Empty<ApplicationInstallationItemDTO>();

        /// <summary>
        /// Indica se a ordena��o foi bem-sucedida.
        /// </summary>
        public bool IsSuccessful { get; init; }

        /// <summary>
        /// Mensagens de erro ou avisos.
        /// </summary>
        public IReadOnlyList<string> Messages { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Aplica��es que n�o puderam ser inclu�das devido a depend�ncias n�o resolvidas.
        /// </summary>
        public IReadOnlyList<UnresolvedApplicationDTO> UnresolvedApplications { get; init; } = Array.Empty<UnresolvedApplicationDTO>();

        /// <summary>
        /// Tempo estimado total de instala��o em minutos.
        /// </summary>
        public int EstimatedTotalTimeMinutes { get; init; }

        /// <summary>
        /// N�mero total de aplica��es a serem instaladas.
        /// </summary>
        public int TotalApplications => Applications.Count;
    }

    /// <summary>
    /// DTO para item de aplica��o na ordem de instala��o.
    /// </summary>
    public class ApplicationInstallationItemDTO
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
        /// Vers�o da aplica��o.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Ordem de instala��o (1 = primeiro).
        /// </summary>
        public int InstallationOrder { get; init; }

        /// <summary>
        /// Prioridade original da aplica��o.
        /// </summary>
        public int Priority { get; init; }

        /// <summary>
        /// Indica se � uma instala��o obrigat�ria.
        /// </summary>
        public bool IsRequired { get; init; }

        /// <summary>
        /// Indica se deve ser instalada em modo silencioso.
        /// </summary>
        public bool IsSilentInstall { get; init; }

        /// <summary>
        /// Tempo estimado de instala��o em minutos.
        /// </summary>
        public int EstimatedInstallTimeMinutes { get; init; }

        /// <summary>
        /// Grupo de instala��o para instala��es paralelas.
        /// </summary>
        public string? InstallationGroup { get; init; }

        /// <summary>
        /// N�vel de profundidade na �rvore de depend�ncias.
        /// </summary>
        public int DependencyLevel { get; init; }

        /// <summary>
        /// IDs das aplica��es das quais esta depende diretamente.
        /// </summary>
        public IReadOnlyList<Guid> DirectDependencies { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Arquivos de instala��o da aplica��o.
        /// </summary>
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Argumentos de instala��o.
        /// </summary>
        public string Arguments { get; init; } = string.Empty;
    }

    /// <summary>
    /// DTO para aplica��es n�o resolvidas.
    /// </summary>
    public class UnresolvedApplicationDTO
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
        /// Motivo pelo qual n�o p�de ser resolvida.
        /// </summary>
        public string Reason { get; init; } = string.Empty;

        /// <summary>
        /// IDs das depend�ncias n�o encontradas.
        /// </summary>
        public IReadOnlyList<Guid> MissingDependencies { get; init; } = Array.Empty<Guid>();
    }

    /// <summary>
    /// DTO para resultado da valida��o de depend�ncias.
    /// </summary>
    public class DependencyValidationResultDTO
    {
        /// <summary>
        /// Indica se as depend�ncias s�o v�lidas (sem ciclos).
        /// </summary>
        public bool IsValid { get; init; }

        /// <summary>
        /// Depend�ncias circulares detectadas.
        /// </summary>
        public IReadOnlyList<CircularDependencyDTO> CircularDependencies { get; init; } = Array.Empty<CircularDependencyDTO>();

        /// <summary>
        /// Aplica��es com depend�ncias n�o encontradas.
        /// </summary>
        public IReadOnlyList<UnresolvedApplicationDTO> MissingDependencies { get; init; } = Array.Empty<UnresolvedApplicationDTO>();

        /// <summary>
        /// Conflitos de aplica��es detectados.
        /// </summary>
        public IReadOnlyList<ApplicationConflictDTO> Conflicts { get; init; } = Array.Empty<ApplicationConflictDTO>();
    }

    /// <summary>
    /// DTO para depend�ncia circular.
    /// </summary>
    public class CircularDependencyDTO
    {
        /// <summary>
        /// Caminho da depend�ncia circular.
        /// </summary>
        public IReadOnlyList<Guid> DependencyPath { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Nomes das aplica��es no ciclo.
        /// </summary>
        public IReadOnlyList<string> ApplicationNames { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Descri��o do ciclo.
        /// </summary>
        public string Description { get; init; } = string.Empty;
    }

    /// <summary>
    /// DTO para conflito de aplica��es.
    /// </summary>
    public class ApplicationConflictDTO
    {
        /// <summary>
        /// ID da primeira aplica��o em conflito.
        /// </summary>
        public Guid ApplicationId1 { get; init; }

        /// <summary>
        /// Nome da primeira aplica��o.
        /// </summary>
        public string ApplicationName1 { get; init; } = string.Empty;

        /// <summary>
        /// ID da segunda aplica��o em conflito.
        /// </summary>
        public Guid ApplicationId2 { get; init; }

        /// <summary>
        /// Nome da segunda aplica��o.
        /// </summary>
        public string ApplicationName2 { get; init; } = string.Empty;

        /// <summary>
        /// Descri��o do conflito.
        /// </summary>
        public string ConflictDescription { get; init; } = string.Empty;
    }

    /// <summary>
    /// DTO para grupos de instala��o paralela.
    /// </summary>
    public class ParallelInstallationGroupsDTO
    {
        /// <summary>
        /// Grupos de aplica��es que podem ser instaladas em paralelo.
        /// </summary>
        public IReadOnlyList<ParallelInstallationGroupDTO> Groups { get; init; } = Array.Empty<ParallelInstallationGroupDTO>();

        /// <summary>
        /// N�mero total de grupos.
        /// </summary>
        public int TotalGroups => Groups.Count;

        /// <summary>
        /// Tempo estimado considerando paralelismo.
        /// </summary>
        public int EstimatedTimeWithParallelism { get; init; }
    }

    /// <summary>
    /// DTO para grupo de instala��o paralela.
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
        /// Aplica��es que podem ser instaladas em paralelo neste grupo.
        /// </summary>
        public IReadOnlyList<ApplicationInstallationItemDTO> Applications { get; init; } = Array.Empty<ApplicationInstallationItemDTO>();

        /// <summary>
        /// Tempo estimado do grupo (aplica��o mais demorada).
        /// </summary>
        public int EstimatedGroupTimeMinutes { get; init; }
    }

    /// <summary>
    /// DTO para estimativa de tempo de instala��o.
    /// </summary>
    public class InstallationTimeEstimateDTO
    {
        /// <summary>
        /// Tempo estimado total em instala��o sequencial.
        /// </summary>
        public int SequentialTimeMinutes { get; init; }

        /// <summary>
        /// Tempo estimado com instala��o paralela.
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
        /// Detalhamento por grupo de instala��o.
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
        /// N�mero de aplica��es no grupo.
        /// </summary>
        public int ApplicationCount { get; init; }
    }
}