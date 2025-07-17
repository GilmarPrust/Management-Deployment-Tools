using API.Control.DTOs.ProfileDeploy;

public interface IDeployProfileService
{
    /// <summary>
    /// Retorna todos os perfis de implantação.
    /// </summary>
    Task<IEnumerable<DeployProfileReadDTO>> GetAllAsync();

    /// <summary>
    /// Retorna um perfil de implantação pelo Id.
    /// </summary>
    Task<DeployProfileReadDTO?> GetByIdAsync(Guid id);

    /// <summary>
    /// Cria um novo perfil de implantação.
    /// </summary>
    Task<DeployProfileReadDTO> CreateAsync(DeployProfileCreateDTO dto);

    /// <summary>
    /// Atualiza um perfil de implantação existente.
    /// </summary>
    Task<bool> UpdateAsync(Guid id, DeployProfileUpdateDTO dto);

    /// <summary>
    /// Remove um perfil de implantação pelo Id.
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
}