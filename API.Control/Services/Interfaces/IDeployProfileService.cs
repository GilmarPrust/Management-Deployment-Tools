using API.Control.DTOs.DeployProfile;

namespace API.Control.Services.Interfaces
{
    public interface IDeployProfileService
    {
        Task<IEnumerable<DeployProfileReadDTO>> GetAllAsync();

        Task<DeployProfileReadDTO?> GetByIdAsync(Guid id);

        Task<DeployProfileReadDTO> CreateAsync(DeployProfileCreateDTO dto);

        Task<bool> UpdateAsync(Guid id, DeployTaskUpdateDTO dto);

        Task<bool> DeleteAsync(Guid id);
    }
}