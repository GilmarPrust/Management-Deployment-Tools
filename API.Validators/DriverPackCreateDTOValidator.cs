using API.DTOs.DriverPack;
using FluentValidation;

namespace API.Validations
{
    public class DriverPackCreateDTOValidator : AbstractValidator<DriverPackCreateDTO>
    {
        public DriverPackCreateDTOValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.OS).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Version).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Source).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Hash).NotEmpty().MaximumLength(64);

            // Regra condicional para DeviceModelId
            RuleFor(x => x.DeviceModelId).NotNull()
                .When(x => x.IsOEM)
                .WithMessage("DeviceModelId é obrigatório quando IsOEM for true.");
        }
    }
}
