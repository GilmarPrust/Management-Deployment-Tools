using DCM.Application.DTOs.Device;
using DCM.Core.Utilities;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de dispositivo.
    /// </summary>
    public class DeviceCreateDTOValidator : AbstractValidator<DeviceCreateDTO>
    {
        public DeviceCreateDTOValidator()
        {
            RuleFor(d => d.ComputerName)
                .NotEmpty().WithMessage("O nome do computador é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do computador deve ter no máximo 100 caracteres.");

            RuleFor(d => d.SerialNumber)
                .MaximumLength(100).WithMessage("O número de série deve ter no máximo 100 caracteres.");

            RuleFor(d => d.MacAddress)
                .NotEmpty().WithMessage("O endereço MAC é obrigatório.")
                .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Formato de endereço MAC inválido.")
                .Must(mac =>
                {
                    try { var _ = new MacAddress(mac); return true; }
                    catch { return false; }
                }).WithMessage("Endereço MAC inválido.");

            RuleFor(d => d.DeviceModelId)
                .NotEmpty().WithMessage("O ID do modelo de dispositivo é obrigatório.");
        }
    }

    /// <summary>
    /// Validador para atualização de dispositivo.
    /// </summary>
    public class DeviceUpdateDTOValidator : AbstractValidator<DeviceUpdateDTO>
    {
        public DeviceUpdateDTOValidator()
        {
            RuleFor(d => d.ComputerName)
                .NotEmpty().WithMessage("O nome do computador é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do computador deve ter no máximo 100 caracteres.");
            RuleFor(d => d.SerialNumber)
                .MaximumLength(100).WithMessage("O número de série deve ter no máximo 100 caracteres.");
            RuleFor(d => d.MacAddress)
                .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Formato de endereço MAC inválido.")
                .Must(mac =>
                {
                    try { var _ = new MacAddress(mac); return true; }
                    catch { return false; }
                }).WithMessage("Endereço MAC inválido.");
            RuleFor(d => d.DeviceModelId)
                .NotEmpty().WithMessage("O ID do modelo de dispositivo é obrigatório.");
        }
    }
}