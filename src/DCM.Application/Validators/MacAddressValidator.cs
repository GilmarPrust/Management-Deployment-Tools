using DCM.Application.DTOs.Device;
using FluentValidation;

namespace DCM.Application.Validators
{
    public class MacAddressValidator : AbstractValidator<DeviceCreateDTO>
    {
        public MacAddressValidator()
        {
            RuleFor(d => d.MacAddress)
            .NotEmpty().WithMessage("MAC address is required.")
            .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").WithMessage("Invalid MAC address format.");
        }
    }
}
