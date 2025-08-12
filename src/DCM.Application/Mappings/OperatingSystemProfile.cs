using AutoMapper;
using DCM.Application.DTOs.OperatingSystem;

namespace DCM.Application.Mappings
{
    public class OperatingSystemProfile : Profile
    {
        public OperatingSystemProfile()
        {
            // Map OperatingSystem to OperatingSystemReadDTO
            CreateMap<DCM.Core.Entities.OperatingSystem, OperatingSystemReadDTO>();
            // Map OperatingSystemCreateDTO to OperatingSystem
            CreateMap<OperatingSystemCreateDTO, Core.Entities.OperatingSystem>();
        }
    }
}
