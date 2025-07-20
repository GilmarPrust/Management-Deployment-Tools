using API.Control.DTOs.DriverPackOEM;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class DriverPackOEMProfile : Profile
    {
        public DriverPackOEMProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DriverPackOEM, DriverPackOEMReadDTO>()
                .ForMember(dest => dest.DeviceModel, opt => opt.MapFrom(src => src.DeviceModel != null ? src.DeviceModel.Id : (Guid?)null));

            // DTO de criação → Entidade
            CreateMap<DriverPackOEMCreateDTO, DriverPackOEM>();

            // DTO de atualização → Entidade
            CreateMap<DriverPackOEMUpdateDTO, DriverPackOEM>();
        }
    }
}
