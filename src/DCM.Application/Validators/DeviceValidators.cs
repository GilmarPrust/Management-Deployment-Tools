using DCM.Application.DTOs.Device;
using DCM.Core.Utilities;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para cria��o de dispositivo.
    /// </summary>
    public class DeviceCreateDTOValidator : AbstractValidator<DeviceCreateDTO>
    {
        public DeviceCreateDTOValidator()
        {
            RuleFor(d => d.ComputerName)
                .NotEmpty().WithMessage("O nome do computador � obrigat�rio.")
                .MaximumLength(100).WithMessage("O nome do computador deve ter no m�ximo 100 caracteres.");

            RuleFor(d => d.SerialNumber)
                .MaximumLength(100).WithMessage("O n�mero de s�rie deve ter no m�ximo 100 caracteres.");

            RuleFor(d => d.MacAddress)
                .NotEmpty().WithMessage("O endere�o MAC � obrigat�rio.")
                .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Formato de endere�o MAC inv�lido.")
                .Must(mac =>
                {
                    try { var _ = new MacAddress(mac); return true; }
                    catch { return false; }
                }).WithMessage("Endere�o MAC inv�lido.");

            RuleFor(d => d.DeviceModelId)
                .NotEmpty().WithMessage("O ID do modelo de dispositivo � obrigat�rio.");
        }
    }

    /// <summary>
    /// Validador para atualiza��o de dispositivo.
    /// </summary>
    public class DeviceUpdateDTOValidator : AbstractValidator<DeviceUpdateDTO>
    {
        public DeviceUpdateDTOValidator()
        {
            RuleFor(d => d.ComputerName)
                .NotEmpty().WithMessage("O nome do computador � obrigat�rio.")
                .MaximumLength(100).WithMessage("O nome do computador deve ter no m�ximo 100 caracteres.");
            RuleFor(d => d.SerialNumber)
                .MaximumLength(100).WithMessage("O n�mero de s�rie deve ter no m�ximo 100 caracteres.");
            RuleFor(d => d.MacAddress)
                .Matches("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Formato de endere�o MAC inv�lido.")
                .Must(mac =>
                {
                    try { var _ = new MacAddress(mac); return true; }
                    catch { return false; }
                }).WithMessage("Endere�o MAC inv�lido.");
            RuleFor(d => d.DeviceModelId)
                .NotEmpty().WithMessage("O ID do modelo de dispositivo � obrigat�rio.");
        }
    }
}