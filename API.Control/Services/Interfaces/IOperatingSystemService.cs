namespace API.Control.Services.Interfaces
{
    public interface IOperatingSystemService
    {
        Task<IEnumerable<OperatingSystemReadDTO>> GetAllAsync();
        Task<OperatingSystemReadDTO?> GetByIdAsync(Guid id);
        Task<OperatingSystemReadDTO> CreateAsync(OperatingSystemCreateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
