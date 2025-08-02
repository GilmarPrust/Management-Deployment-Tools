namespace API.Control.Mappings
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            // Mapping configurations for Inventory
            CreateMap<Inventory, InventoryReadDTO>();

            // DTO to Entity mappings
            CreateMap<InventoryCreateDTO, Inventory>();

        }
    }
}
