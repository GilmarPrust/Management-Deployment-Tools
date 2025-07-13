using API.Control.DTOs.DriverPack;
using API.Control.Models;
using API.Control2.DTOs;
using AutoMapper;

namespace API.Control.Mappings
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
