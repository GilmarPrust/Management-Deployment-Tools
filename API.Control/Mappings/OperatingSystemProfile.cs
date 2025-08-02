namespace API.Control.Mappings
{
    public class OperatingSystemProfile : Profile
    {
        public OperatingSystemProfile()
        {
            // Map OperatingSystem to OperatingSystemReadDTO
            CreateMap<API.Control.Entities.Auxiliary.OperatingSystem, OperatingSystemReadDTO>();
            // Map OperatingSystemCreateDTO to OperatingSystem
            CreateMap<OperatingSystemCreateDTO, API.Control.Entities.Auxiliary.OperatingSystem>();
        }
    }
}
