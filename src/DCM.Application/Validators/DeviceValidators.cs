using DCM.Application.DTOs.Device;
using DCM.Core.ValueObjects;
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
            RuleFor(d => d.DeviceType)
                .IsInEnum().WithMessage("Tipo de dispositivo inv�lido.");

            RuleFor(d => d.SerialNumber)
                .NotEmpty().WithMessage("O n�mero de s�rie � obrigat�rio.")
                .Length(5, 100).WithMessage("O n�mero de s�rie deve ter entre 5 e 100 caracteres.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("N�mero de s�rie deve conter apenas letras mai�sculas, n�meros e h�fens.");

            RuleFor(d => d.MacAddress)
                .NotEmpty().WithMessage("O endere�o MAC � obrigat�rio.")
                .Matches(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Formato de endere�o MAC inv�lido.")
                .Must(mac => {
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
            RuleFor(d => d.DeviceType)
                .IsInEnum().WithMessage("Tipo de dispositivo inv�lido.");

            // ComputerName � obrigat�rio no UpdateDTO com valida��o completa
            RuleFor(d => d.ComputerName)
                .NotEmpty().WithMessage("O nome do computador � obrigat�rio.")
                .MaximumLength(15).WithMessage("O nome do computador deve ter no m�ximo 15 caracteres.")
                .Matches(@"^[A-Z0-9-]+$").WithMessage("Nome do computador deve conter apenas letras mai�sculas, n�meros e h�fens.")
                .Must(name => !name.Contains("--") && !name.StartsWith("-") && !name.EndsWith("-"))
                .WithMessage("Nome do computador n�o pode conter h�fens consecutivos ou come�ar/terminar com h�fen.")
                .Must(IsValidComputerNameFormat)
                .WithMessage("Nome do computador deve seguir o formato PREFIXO-XXXX (ex: DSKTP-A1B2).");

            RuleFor(d => d.SerialNumber)
                .NotEmpty().WithMessage("O n�mero de s�rie � obrigat�rio.")
                .Length(5, 100).WithMessage("O n�mero de s�rie deve ter entre 5 e 100 caracteres.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("N�mero de s�rie deve conter apenas letras mai�sculas, n�meros e h�fens.");

            RuleFor(d => d.MacAddress)
                .NotEmpty().WithMessage("O endere�o MAC � obrigat�rio.")
                .Matches(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Formato de endere�o MAC inv�lido.")
                .Must(mac => {
                    try { var _ = new MacAddress(mac); return true; }
                    catch { return false; }
                }).WithMessage("Endere�o MAC inv�lido.");

            RuleFor(d => d.DeviceModelId)
                .NotEmpty().WithMessage("O ID do modelo de dispositivo � obrigat�rio.");

            RuleFor(d => d.ApplicationIds)
                .NotNull().WithMessage("A lista de IDs de aplica��es n�o pode ser nula.");

            RuleFor(d => d.AppxPackageIds)
                .NotNull().WithMessage("A lista de IDs de pacotes Appx n�o pode ser nula.");

            RuleFor(d => d.DriverPackIds)
                .NotNull().WithMessage("A lista de IDs de pacotes de driver n�o pode ser nula.");
        }

        /// <summary>
        /// Valida se o nome do computador segue o formato esperado (PREFIXO-XXXX).
        /// </summary>
        /// <param name="computerName">Nome do computador a ser validado</param>
        /// <returns>True se o formato for v�lido, false caso contr�rio</returns>
        private static bool IsValidComputerNameFormat(string computerName)
        {
            if (string.IsNullOrWhiteSpace(computerName))
                return false;

            // Formato esperado: PREFIXO-XXXX (ex: DSKTP-A1B2, KIOSK-1234, VM-ABCD)
            var parts = computerName.Split('-');
            
            // Deve ter exatamente 2 partes: prefixo e sufixo
            if (parts.Length != 2)
                return false;

            var prefix = parts[0];
            var suffix = parts[1];

            // Prefixo: 2-5 letras mai�sculas
            if (prefix.Length < 2 || prefix.Length > 5 || !prefix.All(char.IsLetter) || !prefix.Equals(prefix, StringComparison.CurrentCultureIgnoreCase))
                return false;

            // Sufixo: 3-6 caracteres alfanum�ricos
            if (suffix.Length < 3 || suffix.Length > 6 || !suffix.All(char.IsLetterOrDigit) || !suffix.Equals(suffix, StringComparison.CurrentCultureIgnoreCase))
                return false;

            return true;
        }
    }
}