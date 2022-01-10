using Microsoft.AspNetCore.Identity;
using Core.Helpers;

namespace Core.Security
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(LocalizedIdentityErrorMessages.DuplicateEmail.ObterDescricaoEnum(), email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(LocalizedIdentityErrorMessages.DuplicateUserName.ObterDescricaoEnum(), userName)
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format(LocalizedIdentityErrorMessages.InvalidEmail.ObterDescricaoEnum(), email)
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format(LocalizedIdentityErrorMessages.InvalidUserName.ObterDescricaoEnum(), userName)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = LocalizedIdentityErrorMessages.LoginAlreadyAssociated.ObterDescricaoEnum()
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = LocalizedIdentityErrorMessages.PasswordMismatch.ObterDescricaoEnum()
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresDigit.ObterDescricaoEnum()
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresLower.ObterDescricaoEnum()
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresNonAlphanumeric.ObterDescricaoEnum()
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format(LocalizedIdentityErrorMessages.PasswordRequiresUniqueChars.ObterDescricaoEnum(), uniqueChars)
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = LocalizedIdentityErrorMessages.PasswordRequiresUpper.ObterDescricaoEnum()
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format(LocalizedIdentityErrorMessages.PasswordTooShort.ObterDescricaoEnum(), length)
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = LocalizedIdentityErrorMessages.UserAlreadyHasPassword.ObterDescricaoEnum()
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = LocalizedIdentityErrorMessages.DefaultIdentityError.ObterDescricaoEnum()
            };
        }
    }
}