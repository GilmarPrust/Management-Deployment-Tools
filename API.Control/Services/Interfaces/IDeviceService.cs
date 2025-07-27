namespace API.Control.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceReadDTO>> GetAllAsync();
        Task<DeviceReadDTO?> GetByIdAsync(Guid id);
        Task<DeviceReadDTO> CreateAsync(DeviceCreateDTO dto);
        Task<DeviceReadDTO?> UpdateAsync(Guid id, DeviceUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
