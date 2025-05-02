using Application.Dtos.Request;
using FluentValidation;

namespace Application.Validators
{
    public class ContaEdicaoValidator : AbstractValidator<ContaEdicaoInput>
    {
        public ContaEdicaoValidator()
        {
            RuleFor(e => e.Nome).NotEmpty().WithMessage("Campo Descrição não pode estar em branco.");
            RuleFor(e => e.Valor).NotEmpty().WithMessage("Campo Descrição não pode estar em branco.");
            //RuleFor(e => e.ti).IsInEnum();
        }
    }
}
