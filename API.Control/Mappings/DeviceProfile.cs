using API.Control.DTOs.Device;
using API.Control.Models;
using API.Control.ValueObjects;
using AutoMapper;

namespace API.Control.Mappings
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            // DTO de leitura → Entidade
            CreateMap<Device, DeviceReadDTO>();

            // DTO de atualização → Entidade
            CreateMap<DeviceCreateDTO, Device>()
            .ForMember(dest => dest.MacAddress, opt => opt.MapFrom(src => MacAddress.Create(src.MacAddress)));

            // DTO de atualização → Entidade
            CreateMap<DeviceUpdateDTO, Device>();
        }
    }
}
