namespace API.Control.Mappings
{
    public class AppxPackageProfile : Profile
    {
        public AppxPackageProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<AppxPackage, AppxPackageReadDTO>();

            // DTO de criação → Entidade
            CreateMap<AppxPackageCreateDTO, AppxPackage>();

            // DTO de atualização → Entidade
            CreateMap<AppxPackageUpdateDTO, AppxPackage>();
        }
    }
}
