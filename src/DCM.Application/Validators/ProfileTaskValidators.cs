using DCM.Application.DTOs.ProfileTask;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de tarefa de perfil.
    /// </summary>
    public class ProfileTaskCreateDTOValidator : AbstractValidator<ProfileTaskCreateDTO>
    {
        public ProfileTaskCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da tarefa é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.DeployProfileIds)
                .NotNull().WithMessage("A lista de perfis de implantação é obrigatória.")
                .Must(list => list != null && list.Count > 0).WithMessage("Deve haver pelo menos um perfil de implantação.");

            RuleFor(x => x.Phase)
                .IsInEnum().WithMessage("A fase da tarefa é obrigatória.");
        }
    }

    /// <summary>
    /// Validador para atualização de tarefa de perfil.
    /// </summary>
    public class ProfileTaskUpdateDTOValidator : AbstractValidator<ProfileTaskUpdateDTO>
    {
        public ProfileTaskUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da tarefa é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.Enabled)
                .NotNull().WithMessage("O campo Enabled é obrigatório.");

            RuleFor(x => x.Phase)
                .IsInEnum().WithMessage("A fase da tarefa é obrigatória.");
        }
    }
}
