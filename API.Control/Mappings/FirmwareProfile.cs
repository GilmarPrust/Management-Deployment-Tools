namespace API.Control.Mappings
{
    public class FirmwareProfile : Profile
    {
        public FirmwareProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<Firmware, FirmwareReadDTO>();

            // DTO de criação → Entidade
            CreateMap<FirmwareCreateDTO, Firmware>();

            // DTO de atualização → Entidade
            CreateMap<FirmwareUpdateDTO, Firmware>();
        }
    }
}
