namespace API.Control.Services.Interfaces
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerReadDTO>> GetAllAsync();
        Task<ManufacturerReadDTO?> GetByIdAsync(Guid id);
        Task<ManufacturerReadDTO> CreateAsync(ManufacturerCreateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
