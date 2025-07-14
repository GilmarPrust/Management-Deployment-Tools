using API.Control.DTOs.DeviceModel;
using API.Control.DTOs.DriverPack;
using API.Control.DTOs.Firmware;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class DeviceModelProfile : Profile
    {
        public DeviceModelProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DeviceModel, DeviceModelReadDTO>()
                .ForMember(dest => dest.DriverPacks, opt => opt.MapFrom(src => src.DriverPacks))
                .ForMember(dest => dest.Applications, opt => opt.MapFrom(src => src.Applications))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled))
                .ForMember(dest => dest.Firmware, opt => opt.MapFrom(src => src.Firmware));

            // DTO de criação → Entidade
            CreateMap<DeviceModelCreateDTO, DeviceModel>();

            // DTO de atualização → Entidade
            CreateMap<DeviceModelUpdateDTO, DeviceModel>();
        }
    }
}
