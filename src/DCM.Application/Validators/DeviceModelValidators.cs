using DCM.Application.DTOs.DeviceModel;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de modelo de dispositivo.
    /// </summary>
    public class DeviceModelCreateDTOValidator : AbstractValidator<DeviceModelCreateDTO>
    {
        public DeviceModelCreateDTOValidator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("O fabricante é obrigatório.")
                .MaximumLength(100).WithMessage("O fabricante deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(100).WithMessage("O modelo deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Type)
                .MaximumLength(50).WithMessage("O tipo deve ter no máximo 50 caracteres.");
        }
    }

    /// <summary>
    /// Validador para atualização de modelo de dispositivo.
    /// </summary>
    public class DeviceModelUpdateDTOValidator : AbstractValidator<DeviceModelUpdateDTO>
    {
        public DeviceModelUpdateDTOValidator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("O fabricante é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .MaximumLength(50);
        }
    }
}
