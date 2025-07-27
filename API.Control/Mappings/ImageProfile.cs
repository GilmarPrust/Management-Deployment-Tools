namespace API.Control.Mappings
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // Map Image to ImageReadDTO
            CreateMap<Image, ImageReadDTO>()
                .ForMember(dest => dest.DeployProfileIds, opt => opt.MapFrom(src => src.DeployProfiles.Select(dp => dp.Id)));

            // Map ImageCreateDTO to Image
            CreateMap<ManufacturerCreateDTO, Image>();

            // Map ImageUpdateDTO to Image
            CreateMap<ImageUpdateDTO, Image>();
        }
    }
}
