using DCM.Application.DTOs.DriverPack;

namespace DCM.Application.Services.Interfaces
{
    public interface IDriverPackService
    {
        Task<IEnumerable<DriverPackReadDTO>> GetAllAsync();
        Task<DriverPackReadDTO?> GetByIdAsync(Guid id);
        Task<DriverPackReadDTO> CreateAsync(DriverPackCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, DriverPackUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}