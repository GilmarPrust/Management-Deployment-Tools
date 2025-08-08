namespace DCM.Core.Enums
{
    /// <summary>
    /// Fases de execução de uma tarefa de perfil.
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
