using API.Control.DTOs.Image;

namespace API.Control.Services.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageReadDTO>> GetAllAsync();
        Task<ImageReadDTO?> GetByIdAsync(Guid id);
        Task<ImageReadDTO> CreateAsync(ImageCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, ImageUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);

    }
}
