namespace API.Control.Mappings
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            // DTO de leitura → Entidade
            CreateMap<Device, DeviceReadDTO>()
                .ForMember(dest => dest.ApplicationIds, opt => opt.MapFrom(src => src.Applications.Select(app => app.Id)))
                .ForMember(dest => dest.AppxPackageIds, opt => opt.MapFrom(src => src.AppxPackages.Select(app => app.Id)))
                .ForMember(dest => dest.DeployProfileId, opt => opt.MapFrom(src => src.DeployProfile))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled));

            // DTO de atualização → Entidade
            CreateMap<DeviceCreateDTO, Device>()
            .ForMember(dest => dest.MacAddress, opt => opt.MapFrom(src => new MacAddress(src.MacAddress)));

            // DTO de atualização → Entidade
            CreateMap<DeviceUpdateDTO, Device>();
        }
    }
}
