using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelAddFirmwareDTO
    {
        [Required]
        public List<Guid> FirmwareIds { get; set; } = new();
    }
}