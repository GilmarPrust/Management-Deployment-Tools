using API.Control.DTOs.Inventory;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            // Mapping configurations for Inventory
            CreateMap<Inventory, InventoryReadDTO>()
                .ForMember(dest => dest.Info, opt => opt.MapFrom(src => src.Infos.Select(i => i.Id)));

            // DTO to Entity mappings
            CreateMap<InventoryCreateDTO, Inventory>();

            // DTO to Entity mappings for updates
            CreateMap<InventoryUpdateDTO, Inventory>();
        }
    }
}
