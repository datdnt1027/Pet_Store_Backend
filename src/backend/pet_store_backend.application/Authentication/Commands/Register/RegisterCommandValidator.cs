using FluentValidation;

namespace pet_store_backend.application.Authentication.Commands.Register;


public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .Matches("^[a-zA-Z ]*$").WithMessage("First name should only contain letters.")
        .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");
        RuleFor(x => x.LastName)
        .NotEmpty()
        .Matches("^[a-zA-Z ]*$").WithMessage("Last name should only contain letters.")
        .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Password and ConfirmPassword must match");
    }
}