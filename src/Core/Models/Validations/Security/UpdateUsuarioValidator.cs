using Core.Commands.Security;
using FluentValidation;

namespace Core.Models.Validations.Security
{
    public class UpdateUsuarioValidator : AbstractValidator<UpdateUsuarioCommand>
    {
        public UpdateUsuarioValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório!");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
