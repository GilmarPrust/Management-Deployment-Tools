using API.Control.DTOs;
using FluentValidation;

namespace API.Control.Validators;

public class Device_DTO_Validator : AbstractValidator<Device_ReadDTO>
{
    public Device_DTO_Validator()
    {
        RuleFor(d => d.ComputerName)
            .NotEmpty().WithMessage("Computer name is required.")
            .MaximumLength(100).WithMessage("Computer name must be at most 100 characters.");

        RuleFor(d => d.SerialNumber)
            .MaximumLength(100).WithMessage("Serial number must be at most 100 characters.");

        RuleFor(d => d.MacAddress)
            .NotEmpty().WithMessage("MAC address is required.")
            .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").WithMessage("Invalid MAC address format.");

        RuleFor(d => d.DeviceModelId)
            .NotEmpty().WithMessage("Device model ID is required.");
    }
}