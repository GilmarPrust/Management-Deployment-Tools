namespace API.Control.Services.Interfaces
{
    public interface IDriverPackOEMService
    {
        Task<IEnumerable<DriverPackOEMReadDTO>> GetAllAsync();
        Task<DriverPackOEMReadDTO?> GetByIdAsync(Guid id);
        Task<DriverPackOEMReadDTO> CreateAsync(DriverPackOEMCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, DriverPackOEMUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}