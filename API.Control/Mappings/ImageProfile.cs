using API.Control.DTOs.Image;
using API.Control.Models;
using AutoMapper;

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
            CreateMap<ImageCreateDTO, Image>();

            // Map ImageUpdateDTO to Image
            CreateMap<ImageUpdateDTO, Image>();
        }
    }
}
