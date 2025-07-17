using API.Control.DTOs.DeviceModel;
using FluentValidation;

namespace API.Control.Validators
{
    public class DeviceModel_Validator : AbstractValidator<DeviceModelCreateDTO>
    {
        public DeviceModel_Validator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer is required.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.");
        }
    }
}
