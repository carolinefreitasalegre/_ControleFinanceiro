using Application.Dtos.Request;
using FluentValidation;

namespace Application.Validators
{
    public class ContaValidator : AbstractValidator<ContaInput>
    {
        public ContaValidator()
        {
            RuleFor(e => e.Nome).NotEmpty().WithMessage("Campo Descrição não pode estar em branco.");
            RuleFor(e => e.Valor).NotEmpty().WithMessage("Campo Descrição não pode estar em branco.");
            RuleFor(e => e.TipoDeConta).IsInEnum();
    

        }
    }
}
