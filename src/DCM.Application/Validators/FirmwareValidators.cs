using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCM.Application.DTOs.Firmware;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de firmware.
    /// </summary>
    public class FirmwareCreateDTOValidator : AbstractValidator<FirmwareCreateDTO>
    {
        public FirmwareCreateDTOValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("O nome do arquivo do firmware é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("A versão do firmware é obrigatória.")
                .MaximumLength(50);

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("O caminho de origem do firmware é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Hash)
                .NotEmpty().WithMessage("O hash do firmware é obrigatório.")
                .MaximumLength(64);

            RuleFor(x => x.DeviceModelId)
                .NotEmpty().WithMessage("O modelo de dispositivo é obrigatório.");
        }
    }

    /// <summary>
    /// Validador para atualização de firmware.
    /// </summary>
    public class FirmwareUpdateDTOValidator : AbstractValidator<FirmwareUpdateDTO>
    {
        public FirmwareUpdateDTOValidator()
        {
            RuleFor(x => x.Enabled)
                .NotNull().WithMessage("O campo Enabled é obrigatório.");
        }
    }
}
