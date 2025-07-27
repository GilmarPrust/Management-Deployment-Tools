namespace API.Control.Mappings
{
    public class DeviceModelProfile : Profile
    {
        public DeviceModelProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<DeviceModel, DeviceModelReadDTO>();

            // DTO de criação → Entidade
            CreateMap<DeviceModelCreateDTO, DeviceModel>();

            // DTO de atualização → Entidade
            CreateMap<DeviceModelUpdateDTO, DeviceModel>();
        }
    }
}
