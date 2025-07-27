namespace API.Control.Mappings
{
    public class AppxPackageProfile : Profile
    {
        public AppxPackageProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<AppxPackage, AppxPackageReadDTO>()
                .ForMember(dest => dest.DeviceIds, opt => opt.MapFrom(src => src.Devices.Select(d => d.Id)));

            // DTO de criação → Entidade
            CreateMap<AppxPackageCreateDTO, AppxPackage>();

            // DTO de atualização → Entidade
            CreateMap<AppxPackageUpdateDTO, AppxPackage>();
        }
    }
}
