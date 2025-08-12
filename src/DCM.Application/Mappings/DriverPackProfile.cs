using AutoMapper;
using DCM.Application.DTOs.DriverPack;
using DCM.Core.Entities;

namespace DCM.Application.Mappings
{
    public class DriverPackProfile : Profile
    {
        public DriverPackProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DriverPack, DriverPackReadDTO>();

            // DTO de criação → Entidade
            CreateMap<DriverPackCreateDTO, DriverPack>();

            // DTO de atualização → Entidade
            CreateMap<DriverPackUpdateDTO, DriverPack>();
        }
    }
}
