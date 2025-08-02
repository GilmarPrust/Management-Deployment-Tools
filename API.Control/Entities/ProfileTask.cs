namespace API.Control.Entities
{
    /// <summary>
    /// Representa uma tarefa de perfil, utilizada em processos de implantação.
    /// </summary>
    public class ProfileTask : BaseEntity
    {
        /// <summary>
        /// Nome da tarefa de perfil.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da tarefa de perfil.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Fase da tarefa de perfil.
        /// </summary>
        public ProfileTaskPhase Phase { get; set; } = ProfileTaskPhase.WindowsPE;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public ProfileTask() { }

        /// <summary>
        /// Perfis de implantação associados à tarefa de perfil.
        /// </summary>
        public virtual ICollection<DeployProfile> DeployProfiles { get; set; } = new List<DeployProfile>();
    }

    /// <summary>
    /// Enum para as fases de uma ProfileTask.
    /// </summary>
    public enum ProfileTaskPhase
    {
        /// <summary>
        /// Boot e particionamento do disco.
        /// </summary>
        WindowsPE,
        /// <summary>
        /// Injeção de drivers/updates.
        /// </summary>
        OfflineServicing,
        /// <summary>
        /// Remoção de SID com Sysprep.
        /// </summary>
        Generalize,
        /// <summary>
        /// Configurações de sistema.
        /// </summary>
        Specialize,
        /// <summary>
        /// Configuração do usuário final.
        /// </summary>
        OobeSystem,
        /// <summary>
        /// Auditoria do sistema.
        /// </summary>
        AuditSystem,
        /// <summary>
        /// Auditoria do usuário.
        /// </summary>
        AuditUser
    }
}
