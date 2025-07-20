using API.Control.DTOs;
using API.Control.DTOs.Firmware;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class FirmwareProfile : Profile
    {
        public FirmwareProfile()
        {
            // Entidade → DTO de leitura
            CreateMap<Firmware, FirmwareReadDTO>()
                .ForMember(dest => dest.DeviceModel, opt => opt.MapFrom(src => src.DeviceModel != null ? src.DeviceModel.Id : (Guid?)null));

            // DTO de criação → Entidade
            CreateMap<FirmwareCreateDTO, Firmware>();

            // DTO de atualização → Entidade
            CreateMap<FirmwareUpdateDTO, Firmware>();
        }
    }
}
