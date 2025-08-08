namespace API.Control.Validators
{
    public class Device_Validator : AbstractValidator<DeviceCreateDTO>
    {
        public Device_Validator()
        {
            RuleFor(d => d.ComputerName)
            .NotEmpty().WithMessage("Computer name is required.")
            .MaximumLength(100).WithMessage("Computer name must be at most 100 characters.");

            RuleFor(s => s.SerialNumber)
                .MaximumLength(100).WithMessage("Serial number must be at most 100 characters.");

            RuleFor(x => x.MacAddress)
                .NotEmpty()
                .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").Must(mac => {
                    try { new MacAddress(mac); return true; }
                    catch { return false; }
                }).WithMessage("Invalid MAC address format.");

            RuleFor(m => m.DeviceModelId)
                .NotEmpty().WithMessage("Device model ID is required.");

            RuleFor(d => d.MacAddress)
                .NotEmpty().WithMessage("MAC address is required.")
                .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").WithMessage("Invalid MAC address format.");

        }
    }
}