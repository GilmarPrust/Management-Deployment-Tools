namespace API.Control.Validators
{
    public class MacAddress_Validator : AbstractValidator<DeviceCreateDTO>
    {
        public MacAddress_Validator()
        {
            RuleFor(d => d.MacAddress)
            .NotEmpty().WithMessage("MAC address is required.")
            .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$").WithMessage("Invalid MAC address format.");
        }
    }
}
