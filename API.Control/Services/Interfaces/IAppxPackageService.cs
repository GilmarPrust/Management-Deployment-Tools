using API.Control.DTOs.Application;
using API.Control.DTOs.AppxPackage;

namespace API.Control.Services.Interfaces
{
    public interface IAppxPackageService
    {
        Task<IEnumerable<AppxPackageReadDTO>> GetAllAsync();
        Task<AppxPackageReadDTO?> GetByIdAsync(Guid id);
        Task<AppxPackageReadDTO> CreateAsync(AppxPackageCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, AppxPackageUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}