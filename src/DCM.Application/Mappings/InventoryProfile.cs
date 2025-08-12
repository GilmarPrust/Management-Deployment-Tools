using AutoMapper;
using DCM.Application.DTOs.Inventory;
using DCM.Core.Entities;

namespace DCM.Application.Mappings
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
