using Application.Dtos.Request;
using FluentValidation;
using Infrastruture.DataAccess;

namespace Application.Validators
{
    public class UsuarioEdicaoValidator : AbstractValidator<UsuarioEdicaoInput>
    {
        private readonly AppDbContext _context;

        public UsuarioEdicaoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(u => u.Name).NotEmpty().WithMessage("Campo Nome obrigatório.");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Campo Email obrigatório.")
                .EmailAddress().WithMessage("Digite um email válido");

            RuleFor(u => u.Senha).NotEmpty().WithMessage("Campo senha obrigatório.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");
        }
    }
}
