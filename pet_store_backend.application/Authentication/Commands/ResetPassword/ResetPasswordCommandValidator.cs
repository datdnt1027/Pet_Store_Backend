using FluentValidation;
using pet_store_backend.application.Authentication.Commands.ResetPassword;

namespace pet_store_backend.application.Authentication.Commands.Register;


public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Password and ConfirmPassword must match");
    }
}