namespace API.Control.Mappings
{
    public class DriverPackOEMProfile : Profile
    {
        public DriverPackOEMProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DriverPackOEM, DriverPackOEMReadDTO>();

            // DTO de criação → Entidade
            CreateMap<DriverPackOEMCreateDTO, DriverPackOEM>();

            // DTO de atualização → Entidade
            CreateMap<DriverPackOEMUpdateDTO, DriverPackOEM>();
        }
    }
}
