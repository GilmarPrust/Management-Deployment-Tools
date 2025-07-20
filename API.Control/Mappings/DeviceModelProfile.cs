using API.Control.DTOs.DeviceModel;
using API.Control.DTOs.DriverPack;
using API.Control.DTOs.Firmware;
using API.Control.Models;
using AutoMapper;
using System.Linq;

namespace API.Control.Mappings
{
    public class DeviceModelProfile : Profile
    {
        public DeviceModelProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DeviceModel, DeviceModelReadDTO>()
                .ForMember(dest => dest.DriverPacks, opt => opt.MapFrom(src => src.DriverPacksOEM.Select(dp => dp.Id)))
                .ForMember(dest => dest.Firmware, opt => opt.MapFrom(src => src.Firmware != null ? src.Firmware.Id : (Guid?)null))
                .ForMember(dest => dest.ApplicationIds, opt => opt.MapFrom(src => src.Applications.Select(app => app.Id)))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled));


            // DTO de criação → Entidade
            CreateMap<DeviceModelCreateDTO, DeviceModel>();

            // DTO de atualização → Entidade
            CreateMap<DeviceModelUpdateDTO, DeviceModel>();
        }
    }
}
