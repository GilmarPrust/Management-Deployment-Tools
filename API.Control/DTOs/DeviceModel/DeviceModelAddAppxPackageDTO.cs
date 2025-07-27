namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelAddAppxPackageDTO
    {
        [Required]
        public List<Guid> AppxPackageIds { get; set; } = new();
    }
}