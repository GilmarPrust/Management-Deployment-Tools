using DCM.Application.DTOs.AppxPackage;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de AppxPackage.
    /// </summary>
    public class AppxPackageCreateDTOValidator : AbstractValidator<AppxPackageCreateDTO>
    {
        public AppxPackageCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do pacote é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão do pacote é obrigatória.")
                .MaximumLength(50);

            RuleFor(x => x.Publisher)
                .NotEmpty().WithMessage("O publicador do pacote é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.PackageFullName)
                .NotEmpty().WithMessage("O nome completo do pacote é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Status)
                .MaximumLength(50);
        }
    }

    /// <summary>
    /// Validador para atualização de AppxPackage.
    /// </summary>
    public class AppxPackageUpdateDTOValidator : AbstractValidator<AppxPackageUpdateDTO>
    {
        public AppxPackageUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do pacote é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão do pacote é obrigatória.")
                .MaximumLength(50);

            RuleFor(x => x.Publisher)
                .NotEmpty().WithMessage("O publicador do pacote é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.PackageFullName)
                .NotEmpty().WithMessage("O nome completo do pacote é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Status)
                .MaximumLength(50);
        }
    }
}
