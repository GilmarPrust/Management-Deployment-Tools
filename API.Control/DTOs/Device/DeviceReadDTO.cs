using API.Control.DTOs.Application;
using API.Control.DTOs.AppxPackage;
using API.Control.DTOs.DeviceModel;
using API.Control.DTOs.DriverPackage;
using API.Control.Models;
using API.Control2.DTOs;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Device
{
    public class DeviceReadDTO
    {
        public Guid Id { get; init; }
        public string ComputerName { get; init; } = string.Empty;
        public string SerialNumber { get; init; } = string.Empty;
        public string MacAddress { get; init; } = string.Empty;
        public Guid DeviceModelId { get; init; }
        public bool Enabled { get; init; }

        public DeviceModelReadDTO DeviceModel { get; init; } = new();
        public ProfileDeployReadDTO? ProfileDeploy { get; init; } = new ProfileDeployReadDTO();
        public List<ApplicationReadDTO> Applications { get; init; } = new();
        public List<AppxPackageReadDTO> AppxPackages { get; init; } = new();
        public List<DriverPackageReadDTO> DriverPackages { get; init; } = new();
    }
}
