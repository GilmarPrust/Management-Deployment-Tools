using API.Control.DTO.Deploy.Task;
using API.Control.DTOs.DeployProfile;
using API.Control.DTOs.DeployTask;

namespace API.Control.Services.Interfaces
{
    public interface IProfileTaskService
    {
        Task<IEnumerable<ProfileTaskReadDTO>> GetAllAsync();

        Task<ProfileTaskReadDTO?> GetByIdAsync(Guid id);

        Task<ProfileTaskReadDTO> CreateAsync(ProfileTaskCreateDTO dto);

        Task<bool> UpdateAsync(Guid id, ProfileTaskUpdateDTO dto);

        Task<bool> DeleteAsync(Guid id);

    }
}
