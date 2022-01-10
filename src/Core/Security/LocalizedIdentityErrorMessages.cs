using System.ComponentModel;

namespace Core.Security
{
    public enum LocalizedIdentityErrorMessages
    {
        [Description("O email '{0}' já está cadastrado.")] DuplicateEmail,
        [Description("O login '{0}' já está cadastrado.")] DuplicateUserName,
        [Description("O email '{0}' é inválido.")] InvalidEmail,
        [Description("O login '{0}' é inválido.")] InvalidUserName,
        [Description("Já existe um usuário com esse login.")] LoginAlreadyAssociated,
        [Description("Password incorreto.")] PasswordMismatch,
        [Description("Password deve possuir pelo menos um digito ('0'-'9').")] PasswordRequiresDigit,
        [Description("Password deve possuir pelo menos um caracter minusculo ('a'-'z').")] PasswordRequiresLower,
        [Description("Password deve possuir pelo menos um simbolo.")] PasswordRequiresNonAlphanumeric,
        [Description("Password deve possuir pelo menos '{0}' caracter diferente.")] PasswordRequiresUniqueChars,
        [Description("Password deve possuir pelo menos um caracter maiusculo ('A'-'Z').")] PasswordRequiresUpper,
        [Description("Password deve possuir no mínimo '{0}' caracteres.")] PasswordTooShort,
        [Description("O usuário já tem um password definido. ")]UserAlreadyHasPassword,
        [Description("Erro com o usuário. Procure um administrador.")]DefaultIdentityError
    }
}