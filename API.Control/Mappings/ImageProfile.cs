using API.Control.DTOs.Image;
using API.Control.Models;
using API.Control2.DTOs;
using AutoMapper;

namespace API.Control.Mappings
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // Map Image to ImageReadDTO
            CreateMap<Image, ImageReadDTO>();

            // Map ImageCreateDTO to Image
            CreateMap<ImageCreateDTO, Image>();

            // Map ImageUpdateDTO to Image
            CreateMap<ImageUpdateDTO, Image>();
        }
    }
}
