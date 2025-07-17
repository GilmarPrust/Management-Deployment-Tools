using API.Control.DTOs.ProfileDeploy;
using API.Control.Models;
using API.Control2.DTOs;
using AutoMapper;

namespace API.Control.Mappings
{
    public class DeployProfileProfile : Profile
    {
        public DeployProfileProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DeployProfile, DeployProfileReadDTO>();

            // DTO de criação → Entidade
            CreateMap<DeployProfileCreateDTO, DeployProfile>();

            // DTO de atualização → Entidade
            CreateMap<DeployProfileUpdateDTO, DeployProfile>();
        }
    }
}
