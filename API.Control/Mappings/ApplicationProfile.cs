namespace API.Control.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<Application, ApplicationReadDTO>()
                .ForMember(dest => dest.DeviceIds, opt => opt.MapFrom(src => src.Devices.Select(d => d.Id)))
                .ForMember(dest => dest.DeviceModelIds, opt => opt.MapFrom(src => src.DeviceModels.Select(dm => dm.Id)))
                .ForMember(dest => dest.ProfileDeployIds, opt => opt.MapFrom(src => src.DeployProfiles.Select(pd => pd.Id)));

            // DTO de criação → Entidade
            CreateMap<ApplicationCreateDTO, Application>();

            // DTO de atualização → Entidade
            CreateMap<ApplicationUpdateDTO, Application>();
        }
    }
}
