using API.Control.Entities.Auxiliary;

namespace API.Control.Mappings
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<Manufacturer, ManufacturerReadDTO>();
            // DTO de criação → Entidade
            CreateMap<ManufacturerCreateDTO, Manufacturer>();

        }
    }
}
