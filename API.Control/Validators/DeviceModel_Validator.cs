using API.Control.DTOs;
using FluentValidation;

namespace API.Control.Validators
{
    public class DeviceModel_Create_Validator : AbstractValidator<DeviceModel_CreateDTO>
    {
        public DeviceModel_Create_Validator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer is required.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.");
        }
    }
    public class DeviceModel_Update_Validator : AbstractValidator<DeviceModel_UpdateDTO>
    {
        public DeviceModel_Update_Validator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer is required.");
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.");
        }
    }
}
