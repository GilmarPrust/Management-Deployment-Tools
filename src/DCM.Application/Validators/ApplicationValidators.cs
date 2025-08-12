using DCM.Application.DTOs.Application;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para ApplicationCreateDTO.
    /// </summary>
    public class ApplicationCreateDTOValidator : AbstractValidator<ApplicationCreateDTO>
    {
        public ApplicationCreateDTOValidator()
        {
            RuleFor(x => x.NameID)
                .NotEmpty().WithMessage("O campo NameID é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("O campo DisplayName é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("O campo Version é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("O campo FileName é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Argument)
                .MaximumLength(250);

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O campo Source é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Filter)
                .MaximumLength(100);

            RuleFor(x => x.Hash)
                .NotEmpty().WithMessage("O campo Hash é obrigatório.")
                .MaximumLength(64);
        }
    }

    /// <summary>
    /// Validador para ApplicationUpdateDTO.
    /// </summary>
    public class ApplicationUpdateDTOValidator : AbstractValidator<ApplicationUpdateDTO>
    {
        public ApplicationUpdateDTOValidator()
        {
            RuleFor(x => x.NameID)
                .NotEmpty().WithMessage("O campo NameID é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("O campo DisplayName é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("O campo Version é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("O campo FileName é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Argument)
                .MaximumLength(250);

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O campo Source é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Filter)
                .MaximumLength(100);

            RuleFor(x => x.Hash)
                .NotEmpty().WithMessage("O campo Hash é obrigatório.")
                .MaximumLength(64);
        }
    }
}
