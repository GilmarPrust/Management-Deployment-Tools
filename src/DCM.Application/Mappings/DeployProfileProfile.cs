using AutoMapper;
using DCM.Application.DTOs.DeployProfile;
using DCM.Core.Entities;

namespace DCM.Application.Mappings
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
