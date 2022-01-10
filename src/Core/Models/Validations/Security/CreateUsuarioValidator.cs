using Core.Commands.Security;
using FluentValidation;

namespace Core.Models.Validations.Security
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioCommand>
    {
        public CreateUsuarioValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório!");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório!");
            RuleFor(x => x.TipoUsuario).GreaterThanOrEqualTo(0).WithMessage("O valor informado para o campo {PropertyName} é inválido!");
        }
    }
}
