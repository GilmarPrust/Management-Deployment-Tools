namespace API.Control.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryReadDTO>> GetAllAsync();
        Task<InventoryReadDTO?> GetByIdAsync(Guid id);
        Task<InventoryReadDTO> CreateAsync(InventoryCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, InventoryUpdateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
