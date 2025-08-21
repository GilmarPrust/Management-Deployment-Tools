namespace DCM.Core.Enums
{
    /// <summary>
    /// Tipos de dependência entre aplicações.
    /// </summary>
    public enum DependencyType
    {
        /// <summary>
        /// Dependência obrigatória - deve ser instalada antes.
        /// </summary>
        Required = 1,

        /// <summary>
        /// Dependência recomendada - é melhor instalar antes, mas não obrigatório.
        /// </summary>
        Recommended = 2,

        /// <summary>
        /// Dependência opcional - pode ser instalada antes ou depois.
        /// </summary>
        Optional = 3,

        /// <summary>
        /// Conflito - não pode ser instalada junto.
        /// </summary>
        Conflict = 4
    }
}
