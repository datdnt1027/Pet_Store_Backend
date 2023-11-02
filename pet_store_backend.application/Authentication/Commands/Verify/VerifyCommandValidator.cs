using FluentValidation;

namespace pet_store_backend.application.Authentication.Commands.Verify;

public class VerifyQueryValidator : AbstractValidator<VerifyCommand>
{
    public VerifyQueryValidator()
    {
        RuleFor(x => x.VerificationToken).NotEmpty();
    }
}