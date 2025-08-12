using DCM.Application.DTOs.Image;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de imagem de sistema operacional.
    /// </summary>
    public class ImageCreateDTOValidator : AbstractValidator<ImageCreateDTO>
    {
        public ImageCreateDTOValidator()
        {
            RuleFor(x => x.ImageName)
                .NotEmpty().WithMessage("O nome da imagem é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.ImageDescription)
                .NotEmpty().WithMessage("A descrição da imagem é obrigatória.")
                .MaximumLength(250);

            RuleFor(x => x.ImageIndex)
                .InclusiveBetween(1, 100).WithMessage("O índice da imagem deve estar entre 1 e 100.");

            RuleFor(x => x.EditionId)
                .NotEmpty().WithMessage("O identificador da edição é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão é obrigatória.")
                .MaximumLength(20);

            RuleFor(x => x.Languages)
                .NotNull().WithMessage("A lista de idiomas é obrigatória.")
                .Must(l => l.Count > 0).WithMessage("Deve haver pelo menos um idioma.");

            RuleFor(x => x.ImageSize)
                .GreaterThan(0).WithMessage("O tamanho da imagem deve ser maior que zero.");

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O caminho de origem é obrigatório.")
                .MaximumLength(250);

            RuleFor(x => x.OperatingSystemId)
                .NotEmpty().WithMessage("O sistema operacional é obrigatório.");
        }
    }

    /// <summary>
    /// Validador para atualização de imagem de sistema operacional.
    /// </summary>
    public class ImageUpdateDTOValidator : AbstractValidator<ImageUpdateDTO>
    {
        public ImageUpdateDTOValidator()
        {
            RuleFor(x => x.ImageName)
                .NotEmpty().WithMessage("O nome da imagem é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.ImageDescription)
                .NotEmpty().WithMessage("A descrição da imagem é obrigatória.")
                .MaximumLength(250);

            RuleFor(x => x.ImageIndex)
                .InclusiveBetween(1, 100).WithMessage("O índice da imagem deve estar entre 1 e 100.");

            RuleFor(x => x.EditionId)
                .NotEmpty().WithMessage("O identificador da edição é obrigatório.")
                .MaximumLength(50);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão é obrigatória.")
                .MaximumLength(20);

            RuleFor(x => x.Languages)
                .NotNull().WithMessage("A lista de idiomas é obrigatória.")
                .Must(l => l.Count > 0).WithMessage("Deve haver pelo menos um idioma.");

            RuleFor(x => x.ImageSize)
                .GreaterThan(0).WithMessage("O tamanho da imagem deve ser maior que zero.");

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O caminho de origem é obrigatório.")
                .MaximumLength(250);

            RuleFor(x => x.OperatingSystemId)
                .NotEmpty().WithMessage("O sistema operacional é obrigatório.");
        }
    }
}
