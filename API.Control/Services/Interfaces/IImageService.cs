namespace API.Control.Services.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageReadDTO>> GetAllAsync();
        Task<ImageReadDTO?> GetByIdAsync(Guid id);
        Task<ImageReadDTO> CreateAsync(ImageCreateDTO dto);
        Task<ImageReadDTO?> UpdateAsync(Guid id, ImageUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);

    }
}
