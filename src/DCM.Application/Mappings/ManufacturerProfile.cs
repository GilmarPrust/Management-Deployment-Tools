using AutoMapper;
using DCM.Application.DTOs.Manufacturer;
using DCM.Core.Entities;

namespace DCM.Application.Mappings
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<Manufacturer, ManufacturerReadDTO>();
            // DTO de criação → Entidade
            CreateMap<ManufacturerCreateDTO, Manufacturer>();

        }
    }
}
