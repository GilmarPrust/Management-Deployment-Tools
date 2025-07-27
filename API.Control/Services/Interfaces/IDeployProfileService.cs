namespace API.Control.Services.Interfaces
{
    public interface IDeployProfileService
    {
        Task<IEnumerable<DeployProfileReadDTO>> GetAllAsync();
        Task<DeployProfileReadDTO?> GetByIdAsync(Guid id);
        Task<DeployProfileReadDTO> CreateAsync(DeployProfileCreateDTO dto);
        Task<DeployProfileReadDTO?> UpdateAsync(Guid id, DeployProfileUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}