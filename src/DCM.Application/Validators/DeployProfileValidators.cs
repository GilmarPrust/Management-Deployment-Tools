using FluentValidation;
using DCM.Application.DTOs.AppxPackage.Devices;
using DCM.Application.DTOs.DeployProfile;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de perfil de implantação.
    /// </summary>
    public class DeployProfileCreateDTOValidator : AbstractValidator<DeployProfileCreateDTO>
    {
        public DeployProfileCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do perfil é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250);

            RuleFor(x => x.ImageId)
                .NotEmpty().WithMessage("O ID da imagem é obrigatório.");
        }
    }

    /// <summary>
    /// Validador para atualização de perfil de implantação.
    /// </summary>
    public class DeployProfileUpdateDTOValidator : AbstractValidator<DeployProfileUpdateDTO>
    {
        public DeployProfileUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do perfil é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250);

            RuleFor(x => x.ImageId)
                .NotEmpty().WithMessage("O ID da imagem é obrigatório.");
        }
    }

    /// <summary>
    /// Validador para atualização de dispositivos associados ao perfil.
    /// </summary>
    public class AppxPackageDevicesUpdateDTOValidator : AbstractValidator<AppxPackageDevicesUpdateDTO>
    {
        public AppxPackageDevicesUpdateDTOValidator()
        {
            RuleFor(x => x.DeviceIds)
                .NotNull().WithMessage("A lista de dispositivos não pode ser nula.");
        }
    }

    /// <summary>
    /// Validador para adição de dispositivo ao perfil.
    /// </summary>
    public class AppxPackageDevicesAddDTOValidator : AbstractValidator<AppxPackageDevicesAddDTO>
    {
        public AppxPackageDevicesAddDTOValidator()
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("O ID do dispositivo é obrigatório.");
        }
    }
}
