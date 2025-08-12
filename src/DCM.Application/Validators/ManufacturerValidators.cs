using DCM.Application.DTOs.Manufacturer;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de fabricante.
    /// </summary>
    public class ManufacturerCreateDTOValidator : AbstractValidator<ManufacturerCreateDTO>
    {
        public ManufacturerCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do fabricante é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.ShortName)
                .NotEmpty().WithMessage("O nome curto do fabricante é obrigatório.")
                .MaximumLength(5);
        }
    }
}
