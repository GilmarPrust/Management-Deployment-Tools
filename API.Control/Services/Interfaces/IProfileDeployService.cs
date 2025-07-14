using API.Control.DTOs.ProfileDeploy;

public interface IProfileDeployService
{
    Task<IEnumerable<ProfileDeployReadDTO>> GetAllAsync();
    Task<ProfileDeployReadDTO?> GetByIdAsync(Guid id);
    Task<ProfileDeployReadDTO> CreateAsync(ProfileDeployCreateDTO dto);
    Task<bool> UpdateAsync(Guid id, ProfileDeployUpdateDTO dto);
    Task<bool> DeleteAsync(Guid id);
}