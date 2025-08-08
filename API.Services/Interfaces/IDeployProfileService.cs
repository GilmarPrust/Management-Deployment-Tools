using DCM.DTOs.DeployProfile;

namespace API.Interfaces
{
    public interface IDeployProfileService
    {
        Task<IEnumerable<DeployProfileReadDTO>> GetAllAsync();
        Task<DeployProfileReadDTO?> GetByIdAsync(Guid id);
        Task<DeployProfileReadDTO> CreateAsync(DeployProfileCreateDTO dto);
        Task<DeployProfileReadDTO?> UpdateAsync(Guid id, DeployProfileUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateDevicesAsync(Guid id, IReadOnlyList<Guid> deviceIds);
        Task<bool> AddDeviceAsync(Guid id, Guid deviceId);
        Task<bool> RemoveDeviceAsync(Guid id, Guid deviceId);
        Task<object?> GetDevicesByDeployProfileIdAsync(Guid id);
        Task<object?> GetApplicationsByDeployProfileIdAsync(Guid id);
        Task<bool> UpdateApplicationsAsync(Guid id, object applicationIds);
    }
}