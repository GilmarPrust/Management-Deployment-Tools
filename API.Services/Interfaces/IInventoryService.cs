using DCM.DTOs.Inventory;

namespace API.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryReadDTO>> GetAllAsync();
        Task<InventoryReadDTO?> GetByIdAsync(Guid id);
        Task<InventoryReadDTO> CreateAsync(InventoryCreateDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
