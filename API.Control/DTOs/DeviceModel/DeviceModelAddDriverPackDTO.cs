using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelAddDriverPackDTO
    {
        [Required]
        public List<Guid> DriverPackIds { get; set; } = new();
    }
}