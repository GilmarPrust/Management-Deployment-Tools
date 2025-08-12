using DCM.Application.DTOs.Application;

namespace DCM.Application.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationReadDTO>> GetAllAsync();
        Task<ApplicationReadDTO?> GetByIdAsync(Guid id);
        Task<ApplicationReadDTO> CreateAsync(ApplicationCreateDTO dto);
        Task<ApplicationReadDTO?> UpdateAsync(Guid id, ApplicationUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}