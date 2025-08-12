using DCM.Application.DTOs.Inventory;
using FluentValidation;

namespace DCM.Application.Validators
{
    /// <summary>
    /// Validador para criação de inventário.
    /// </summary>
    public class InventoryCreateDTOValidator : AbstractValidator<InventoryCreateDTO>
    {
        public InventoryCreateDTOValidator()
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("O identificador do dispositivo é obrigatório.");
        }
    }
}
