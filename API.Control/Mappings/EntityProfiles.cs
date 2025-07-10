using API.Control.DTOs;
using API.Control.Models;
using AutoMapper;

namespace API.Control.Mappings
{
    public class EntityProfiles : Profile
    {
        public EntityProfiles()
        {
            // pedir p gerar o AutoMapper para mapear as entidades e DTOs
            CreateMap<Device, Device_DTO>()
                .ForMember(dest => dest.ComputerName, opt => opt.MapFrom(src => src.ComputerName.Value))
                .ForMember(dest => dest.MacAddress, opt => opt.MapFrom(src => src.MacAddress.Value))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ReverseMap();

            CreateMap<ProfileDeploy, Device_WriteDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ImageName))
                .ForMember(dest => dest.ImageDescription, opt => opt.MapFrom(src => src.Image.ImageDescription))
                .ForMember(dest => dest.ImageIndex, opt => opt.MapFrom(src => src.Image.ImageIndex))
                .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.Image.ShortName))
                .ForMember(dest => dest.EditionId, opt => opt.MapFrom(src => src.Image.EditionId))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Image.Version))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Image.Languages))
                .ForMember(dest => dest.ImageSize, opt => opt.MapFrom(src => src.Image.ImageSize))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Image.Source))
                .ReverseMap();

            CreateMap<Application, Application_DTO>()
                .ForMember(dest => dest.NameID, opt => opt.MapFrom(src => src.NameID))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Argument, opt => opt.MapFrom(src => src.Argument))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => src.Filter))
                .ForMember(dest => dest.Hash, opt => opt.MapFrom(src => src.Hash))
                .ReverseMap();
        }
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EntityProfiles>());
            return config.CreateMapper();
        }
        public static IMapper Mapper { get; } = CreateMapper();
        public static void InitializeMapper()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
        public static void Initialize()
        {
            InitializeMapper();
        }
    }
}
