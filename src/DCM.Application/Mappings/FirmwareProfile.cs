using AutoMapper;
using DCM.Application.DTOs.Firmware;
using DCM.Core.Entities;

namespace DCM.Application.Mappings
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
