using API.Control.DTOs.DeviceModel;
using FluentValidation;

namespace API.Control.Validators
{
    public class DeviceModel_Create_Validator : AbstractValidator<DeviceModelCreateDTO>
    {
        public DeviceModel_Create_Validator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer is required.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.");
        }
    }
    public class DeviceModel_Update_Validator : AbstractValidator<DeviceModelUpdateDTO>
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
