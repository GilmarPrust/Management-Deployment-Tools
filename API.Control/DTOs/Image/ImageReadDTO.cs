using API.Control.DTOs.Application;
using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class ImageReadDTO
    {
        public Guid Id { get; init; }
        public string ImageName { get; init; } = string.Empty;
        public string ImageDescription { get; init; } = string.Empty;
        public string ImageIndex { get; init; } = string.Empty;
        public string ShortName { get; init; } = string.Empty;
        public string EditionId { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string[] Languages { get; init; } = Array.Empty<string>();
        public long ImageSize { get; init; }
        public string Source { get; init; } = string.Empty;

        public List<ProfileDeployReadDTO> ProfilesDeploy { get; init; } = new();
    }
}
