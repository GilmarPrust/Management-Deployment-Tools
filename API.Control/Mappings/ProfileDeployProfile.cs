using API.Control.DTOs.ProfileDeploy;
using API.Control.Models;
using API.Control2.DTOs;
using AutoMapper;

namespace API.Control.Mappings
{
    public class ProfileDeployProfile : Profile
    {
        public ProfileDeployProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<ProfileDeploy, ProfileDeployReadDTO>();

            // DTO de criação → Entidade
            CreateMap<ProfileDeployCreateDTO, ProfileDeploy>();

            // DTO de atualização → Entidade
            CreateMap<ProfileDeployUpdateDTO, ProfileDeploy>();
        }
    }
}
