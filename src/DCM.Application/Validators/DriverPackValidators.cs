using DCM.Application.DTOs.DriverPack;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de pacote de driver.
    /// </summary>
    public class DriverPackCreateDTOValidator : AbstractValidator<DriverPackCreateDTO>
    {
        public DriverPackCreateDTOValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("O nome do arquivo é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.OS)
                .NotEmpty().WithMessage("O sistema operacional é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão é obrigatória.")
                .MaximumLength(50);

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O caminho de origem é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Hash)
                .NotEmpty().WithMessage("O hash é obrigatório.")
                .MaximumLength(64);

            RuleFor(x => x.DeviceModelId)
                .NotEmpty().WithMessage("O modelo de dispositivo é obrigatório.");

            RuleFor(x => x.IsOEM)
                .NotNull().WithMessage("O campo IsOEM é obrigatório.");
        }
    }

    /// <summary>
    /// Validador para atualização de pacote de driver.
    /// </summary>
    public class DriverPackUpdateDTOValidator : AbstractValidator<DriverPackUpdateDTO>
    {
        public DriverPackUpdateDTOValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("O nome do arquivo é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.OS)
                .NotEmpty().WithMessage("O sistema operacional é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão é obrigatória.")
                .MaximumLength(50);

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O caminho de origem é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Hash)
                .NotEmpty().WithMessage("O hash é obrigatório.")
                .MaximumLength(64);

            RuleFor(x => x.DeviceModelId)
                .NotEmpty().WithMessage("O modelo de dispositivo é obrigatório.");

            RuleFor(x => x.IsOEM)
                .NotNull().WithMessage("O campo IsOEM é obrigatório.");
        }
    }
}
