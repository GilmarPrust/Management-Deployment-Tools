using API.Control.DTOs.DeviceModel;

namespace API.Control.Services.Interfaces
{
    public interface IDeviceModelService
    {
        Task<IEnumerable<DeviceModelReadDTO>> GetAllAsync();
        Task<DeviceModelReadDTO?> GetByIdAsync(Guid id);
        Task<DeviceModelReadDTO> CreateAsync(DeviceModelCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, DeviceModelUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddApplicationsAsync(Guid id, List<Guid> applicationIds);
    }
}
