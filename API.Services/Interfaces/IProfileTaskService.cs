using DCM.DTOs.ProfileTask;

namespace API.Interfaces
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
