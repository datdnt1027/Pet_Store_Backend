using FluentValidation;

namespace pet_store_backend.application.Authentication.Commands.ForgotPassword;


public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}