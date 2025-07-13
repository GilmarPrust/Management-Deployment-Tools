using API.Control.DTOs.Application;

namespace API.Control.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationReadDTO>> GetAllAsync();
        Task<ApplicationReadDTO?> GetByIdAsync(Guid id);
        Task<ApplicationReadDTO> CreateAsync(ApplicationCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, ApplicationUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}