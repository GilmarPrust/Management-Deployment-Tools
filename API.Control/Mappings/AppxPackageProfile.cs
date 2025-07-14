using API.Control.DTOs.AppxPackage;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class AppxPackageProfile : Profile
    {
        public AppxPackageProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<AppxPackage, AppxPackageReadDTO>()
                .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices));

            // DTO de criação → Entidade
            CreateMap<AppxPackageCreateDTO, AppxPackage>();

            // DTO de atualização → Entidade
            CreateMap<AppxPackageUpdateDTO, AppxPackage>();
        }
    }
}
