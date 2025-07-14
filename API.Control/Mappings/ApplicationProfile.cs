using API.Control.DTOs.Application;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<Application, ApplicationReadDTO>()
                .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices))
                .ForMember(dest => dest.DeviceModels, opt => opt.MapFrom(src => src.DeviceModels))
                .ForMember(dest => dest.ProfileDeploys, opt => opt.MapFrom(src => src.ProfileDeploys));

            // DTO de criação → Entidade
            CreateMap<ApplicationCreateDTO, Application>();

            // DTO de atualização → Entidade
            CreateMap<ApplicationUpdateDTO, Application>();
        }
    }
}
