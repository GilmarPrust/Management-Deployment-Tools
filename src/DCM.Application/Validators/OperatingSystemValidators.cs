using DCM.Application.DTOs.OperatingSystem;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de sistema operacional.
    /// </summary>
    public class OperatingSystemCreateDTOValidator : AbstractValidator<OperatingSystemCreateDTO>
    {
        public OperatingSystemCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do sistema operacional é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.ShortName)
                .NotEmpty().WithMessage("O nome curto do sistema operacional é obrigatório.")
                .MaximumLength(5);
        }
    }
}
