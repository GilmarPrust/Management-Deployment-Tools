using AutoMapper;
using DCM.Application.DTOs.ProfileTask;
using DCM.Core.Entities;

namespace DCM.Application.Mappings
{
    public class ProfileTaskProfile : Profile
    {
        public ProfileTaskProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<ProfileTask, ProfileTaskReadDTO>();

            // DTO de criação → Entidade
            CreateMap<ProfileTaskCreateDTO, ProfileTask>();

            // DTO de atualização → Entidade
            CreateMap<ProfileTaskUpdateDTO, ProfileTask>();
        }
    }
}
