using API.Control.DTOs.Device;
using FluentValidation;

namespace API.Control.Validators;

public class Device_Create_Validator : AbstractValidator<DeviceCreateDTO>
{
    public Device_Create_Validator()
    {
        RuleFor(d => d.ComputerName)
        .NotEmpty().WithMessage("Computer name is required.")
        .MaximumLength(100).WithMessage("Computer name must be at most 100 characters.");

        RuleFor(s => s.SerialNumber)
            .MaximumLength(100).WithMessage("Serial number must be at most 100 characters.");

        RuleFor(x => x.MacAddress)
            .NotEmpty()
            .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").Must(mac => {
                try { API.Control.ValueObjects.MacAddress.Create(mac); return true; }
                catch { return false; }
            }).WithMessage("Invalid MAC address format.");

        RuleFor(m => m.DeviceModelId)
            .NotEmpty().WithMessage("Device model ID is required.");
    }
}
public class Device_Update_Validator : AbstractValidator<DeviceUpdateDTO>
{
    public Device_Update_Validator()
    {
        RuleFor(d => d.ComputerName)
        .NotEmpty().WithMessage("Computer name is required.")
        .MaximumLength(100).WithMessage("Computer name must be at most 100 characters.");

        RuleFor(s => s.SerialNumber)
            .MaximumLength(100).WithMessage("Serial number must be at most 100 characters.");

        RuleFor(x => x.MacAddress)
            .NotEmpty()
            .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").Must(mac => {
                try { API.Control.ValueObjects.MacAddress.Create(mac); return true; }
                catch { return false; }
            }).WithMessage("Invalid MAC address format.");

        RuleFor(m => m.DeviceModelId)
            .NotEmpty().WithMessage("Device model ID is required.");
    }
}