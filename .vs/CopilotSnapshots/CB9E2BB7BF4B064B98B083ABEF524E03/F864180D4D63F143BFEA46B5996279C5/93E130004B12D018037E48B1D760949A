using DCM.Application.DTOs.DeviceModel;

namespace DCM.Application.Services.Interfaces
{
    public interface IDeviceModelService
    {
        Task<IEnumerable<DeviceModelReadDTO>> GetAllAsync();
        Task<DeviceModelReadDTO?> GetByIdAsync(Guid id);
        Task<DeviceModelReadDTO> CreateAsync(DeviceModelCreateDTO dto);
        Task<DeviceModelReadDTO?> UpdateAsync(Guid id, DeviceModelUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);

        Task<bool> AddApplicationsAsync(Guid id, List<Guid> applicationIds);
        Task<bool> AddDriverPackAsync(Guid id, List<Guid> DriverPackIds);
        Task<bool> AddFirmwareAsync(Guid id, List<Guid> FirmwareIds);
        Task<bool> AddDeviceAsync(Guid id, List<Guid> DeviceIds);

    }
}
