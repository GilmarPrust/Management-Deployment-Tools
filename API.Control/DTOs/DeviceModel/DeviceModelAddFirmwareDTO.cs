namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelAddFirmwareDTO
    {
        [Required]
        public List<Guid> FirmwareIds { get; set; } = new();
    }
}