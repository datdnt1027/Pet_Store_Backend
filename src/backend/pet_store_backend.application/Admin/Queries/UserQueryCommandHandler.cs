using System.Text.RegularExpressions;
using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Customer.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Admin.Queries;

public record UserQuery(
// string Email,
// string PhoneNumber
) : IRequest<ErrorOr<List<UserProfileWithStatusResult>>>;

public class UserQueryValidator : AbstractValidator<UserQuery>
{
    public UserQueryValidator()
    {
        // RuleFor(query => query.Email)
        //     .EmailAddress().WithMessage("Invalid email address.")
        //     .When(query => query.Email != null);

        // RuleFor(query => query.PhoneNumber)
        //     .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format. Include country code and digits only.")
        //     .When(query => query.PhoneNumber != null);
    }

    // private bool BeValidPhoneNumber(string phoneNumber)
    // {
    //     // Allow null or a valid phone number format
    //     return phoneNumber == null || Regex.IsMatch(phoneNumber, @"^\+\d{1,3}(\d{10})$");
    // }
}

public class UserQueryHandler : IRequestHandler<UserQuery, ErrorOr<List<UserProfileWithStatusResult>>>
{
    private readonly IUserRepository _userRepository;

    public UserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<UserProfileWithStatusResult>>> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        var listUser = await _userRepository.GetListUser();
        return listUser;
    }

}