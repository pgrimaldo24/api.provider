using FluentValidation;

namespace Ripley.Api.Provider.Application.Auth.Commands.UserAuthentication
{
    public class UserAuthenticationCommandValidator : AbstractValidator<UserAuthenticationCommand>
    {
        public UserAuthenticationCommandValidator()
        {
            RuleFor(p => p.User)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

        }
    }
}
