using API.Control.DTOs;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings;

public class EntityMappingProfile : Profile
{
    public EntityMappingProfile()
    {
        CreateMap<Device, Device_ReadDTO>()
            .ForMember(dest => dest.ComputerName, opt => opt.MapFrom(src => src.ComputerName.Value))
            .ForMember(dest => dest.MacAddress, opt => opt.MapFrom(src => src.MacAddress.Value))
            .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber));

        CreateMap<Device_CreateDTO, Device>()
            .ConstructUsing(dto => new Device(dto.ComputerName, dto.SerialNumber, dto.MacAddress))
            .ForMember(dest => dest.DeviceModelId, opt => opt.MapFrom(src => src.DeviceModelId));
    }
}