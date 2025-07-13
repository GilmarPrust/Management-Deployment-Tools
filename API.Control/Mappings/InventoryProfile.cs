using API.Control.DTOs.Inventory;
using API.Control.Models;
using API.Control2.DTOs;
using AutoMapper;

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

            // DTO to Entity mappings for updates
            CreateMap<InventoryUpdateDTO, Inventory>();
        }
    }
}
