using API.Control.DTOs.DriverPackage;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class DriverPackageProfile : Profile
    {
        public DriverPackageProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DriverPackage, DriverPackageReadDTO>();

            // DTO de criação → Entidade
            CreateMap<DriverPackageCreateDTO, DriverPackage>();

            // DTO de atualização → Entidade
            CreateMap<DriverPackageUpdateDTO, DriverPackage>();
        }
    }
}
