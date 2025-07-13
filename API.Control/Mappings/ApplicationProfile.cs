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
            CreateMap<Application, ApplicationReadDTO>();

            // DTO de criação → Entidade
            CreateMap<ApplicationCreateDTO, Application>();

            // DTO de atualização → Entidade
            CreateMap<ApplicationUpdateDTO, Application>();
        }
    }
}
