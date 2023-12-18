using System.Text.RegularExpressions;
using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.User.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Admin.Queries;

public record CustomerQuery(
    string Email,
    string PhoneNumber
) : IRequest<ErrorOr<UserProfileWithStatusResult>>;

public class CustomerQueryValidator : AbstractValidator<CustomerQuery>
{
    public CustomerQueryValidator()
    {
        RuleFor(query => query.Email)
            .EmailAddress().WithMessage("Invalid email address.")
            .When(query => query.Email != null);

        RuleFor(query => query.PhoneNumber)
            .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format. Include country code and digits only.")
            .When(query => query.PhoneNumber != null);
    }

    private bool BeValidPhoneNumber(string phoneNumber)
    {
        // Allow null or a valid phone number format
        return phoneNumber == null || Regex.IsMatch(phoneNumber, @"^\+\d{1,3}(\d{10})$");
    }
}

public class CustomerQueryHandler : IRequestHandler<CustomerQuery, ErrorOr<UserProfileWithStatusResult>>
{
    private readonly IUserRepository _userRepository;

    public CustomerQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserProfileWithStatusResult>> Handle(CustomerQuery request, CancellationToken cancellationToken)
    {
        if (request.Email == null && request.PhoneNumber == null)
        {
            return Errors.User.UserNotExist;
        }

        UserProfileWithStatusResult? customer = null;

        if (request.Email != null)
        {
            customer = await _userRepository.GetCustomerByEmailForAdmin(request.Email);
        }

        if (customer is null && request.PhoneNumber != null)
        {
            customer = await _userRepository.GetCustomerByPhoneNumberForAdmin(request.PhoneNumber);
        }

        return customer is not null ? customer : Errors.User.UserNotExist;
    }

}