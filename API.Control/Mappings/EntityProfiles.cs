using API.Control.DTOs;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class EntityProfiles : Profile
    {
        public EntityProfiles()
        {
            // Mapeamento de entrada para entidade
            CreateMap<DeviceModel_CreateDTO, DeviceModel>()
                .ConstructUsing(dto => new DeviceModel(dto.Manufacturer, dto.Model, dto.Type)
                {
                    FirmwareId = dto.FirmwareId,
                    Applications = new List<Application>(),
                    DriverPacks = new List<DriverPack>()
                });

            // Mapeamento de entidade para leitura
            CreateMap<DeviceModel, DeviceModel_ReadDTO>()
                .ForMember(dest => dest.ApplicationsId,
                    opt => opt.MapFrom(src => src.Applications.Select(a => a.Id)))
                .ForMember(dest => dest.DriverPacksId,
                    opt => opt.MapFrom(src => src.DriverPacks.Select(d => d.Id)));
        }
    }

}
