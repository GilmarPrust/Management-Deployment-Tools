namespace API.Control.Mappings
{
    public class DeployProfileProfile : Profile
    {
        public DeployProfileProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DeployProfile, DeployProfileReadDTO>()
                .ForMember(dest => dest.DeviceIds, opt => opt.MapFrom(src => src.Devices.Select(d => d.Id)));

            // DTO de criação → Entidade
            CreateMap<DeployProfileCreateDTO, DeployProfile>();

            // DTO de atualização → Entidade
            CreateMap<DeployProfileUpdateDTO, DeployProfile>();
        }
    }
}
