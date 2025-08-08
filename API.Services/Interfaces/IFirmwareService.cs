using DCM.DTOs.Firmware;

namespace API.Interfaces
{
    public interface IFirmwareService
    {
        Task<IEnumerable<FirmwareReadDTO>> GetAllAsync();
        Task<FirmwareReadDTO?> GetByIdAsync(Guid id);
        Task<FirmwareReadDTO> CreateAsync(FirmwareCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, FirmwareUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
