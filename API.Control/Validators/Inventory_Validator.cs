using FluentValidation;
using API.Control.DTOs.Inventory;

namespace API.Control.Validators
{
    public class Inventory_Validator : AbstractValidator<InventoryUpdateDTO>
    {
        public Inventory_Validator()
        {
            RuleFor(x => x.Data)
                .NotNull().WithMessage("O campo Data é obrigatório.")
                .Must(d => d.Count > 0).WithMessage("O campo Data deve conter pelo menos um item.");
        }
    }
}
