using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Utils;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.application.Admin.Commands;

public record CreateUserCommand(
    string UserRoleId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
) : IRequest<ErrorOr<MessageResult>>;
public class RegisterCommandValidator : AbstractValidator<CreateUserCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserRoleId)
        .NotEmpty().WithMessage("UserRole ID is required.")
        .Must(BeAValidGuid).WithMessage("UserRole ID must be a valid GUID.");

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

    private static bool BeAValidGuid(string categoryId)
    {
        return Guid.TryParse(categoryId, out _);
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<MessageResult>>
{
    private IUserRepository _userRepository;
    private readonly IPasswordConfiguration _passwordConfiguration;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordConfiguration passwordConfiguration)
    {
        _userRepository = userRepository;
        _passwordConfiguration = passwordConfiguration;
    }

    public async Task<ErrorOr<MessageResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckUserRoleExist(Guid.Parse(request.UserRoleId)))
        {
            return Errors.UserRole.UserRoleNotExist;
        }
        // Validate User doesn't exist
        if (await _userRepository.CheckUserExistByEmail(request.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        _passwordConfiguration.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        // Create user (generate unique Id) & Persist to DB
        var user = pet_store_backend.domain.Entities.Users.User.Create
        (
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            passwordSalt,
            UserRoleId.Create(Guid.Parse(request.UserRoleId))
        );
        user.CreateVerificationToken(_passwordConfiguration.CreateRandomToken(), DateTime.Now.AddMinutes(HttpContextItemKeys.ExpireTokenInMinutes));
        user.UpdateVerifiedAt(DateTime.Now);
        await _userRepository.AddUser(user);

        return new MessageResult("User successfully created.!");
    }
}