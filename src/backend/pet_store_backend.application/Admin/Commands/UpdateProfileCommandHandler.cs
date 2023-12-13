using System.Security.Claims;
using System.Text.RegularExpressions;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.application.Admin.Commands;

public record UpdateAdminProfileCommand(
    string FirstName,
    string LastName,
    string Sex,
    // string Email,
    string Address,
    byte[] Avatar,
    string PhoneNumber
) : IRequest<ErrorOr<MessageResult>>;

public class UpdateAdminProfileCommandValidator : AbstractValidator<UpdateAdminProfileCommand>
{
    public UpdateAdminProfileCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .Matches("^[a-zA-Z ]*$").WithMessage("First name should only contain letters.")
            .When(command => command.FirstName != null);

        RuleFor(command => command.LastName)
            .Matches("^[a-zA-Z ]*$").WithMessage("Last name should only contain letters.")
            .When(command => command.LastName != null);

        RuleFor(command => command.Sex)
            .NotEmpty()
            .Must(BeValidGender).WithMessage("Invalid gender value.");

        // RuleFor(command => command.Email)
        //     .EmailAddress().WithMessage("Invalid email address.")
        //     .When(command => command.Email != null);

        RuleFor(command => command.Address)
            .Matches("^[a-zA-Z0-9 ,/]*$").WithMessage("Invalid address format.")
            .When(command => command.Address != null);

        RuleFor(command => command.Avatar)
            .Must(BeValidAvatar).WithMessage("Invalid avatar.")
            .When(command => command.Avatar != null);

        RuleFor(command => command.PhoneNumber)
            .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format. Include country code and digits only.")
            .When(command => command.PhoneNumber != null);
    }

    private bool BeValidGender(string sex)
    {
        return Enum.IsDefined(typeof(Gender), int.Parse(sex));
    }

    private bool BeValidAvatar(byte[] avatar)
    {
        // Allow null or non-empty byte array
        return avatar == null || avatar.Length > 0;
    }

    private bool BeValidPhoneNumber(string phoneNumber)
    {
        // Allow null or a valid phone number format
        return phoneNumber == null || Regex.IsMatch(phoneNumber, @"^\+\d{1,3}(\d{10})$");
    }
}

public class AdminProfileAdminCommandHandler : IRequestHandler<UpdateAdminProfileCommand, ErrorOr<MessageResult>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminProfileAdminCommandHandler(IAdminRepository adminRepository, IHttpContextAccessor httpContextAccessor)
    {
        _adminRepository = adminRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<MessageResult>> Handle(UpdateAdminProfileCommand command, CancellationToken cancellationToken)
    {
        bool flag = false;
        // Check if all properties in the command are null
        if (AllPropertiesNull(command))
        {
            return Errors.User.NoUserInfoUpdate;
        }

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Errors.User.UserNotSignIn;
        }
        // Retrieve the existing admin profile from the repository
        var user = await _adminRepository.RetrieveUser(Guid.Parse(userId)); // Replace with the actual method

        if (user is null)
        {
            return Errors.User.UserNotExist;
        }

        // Check and update properties only if they are different in AdminProfileAdminCommand
        if (command.FirstName != null && command.FirstName != user.FirstName)
        {
            if (!flag) flag = true;
            user.UpdateFirstName(command.FirstName);
        }

        if (command.LastName != null && command.LastName != user.LastName)
        {
            if (!flag) flag = true;
            user.UpdateLastName(command.LastName);
        }

        // if (command.Email != null && command.Email != user.Email)
        // {
        //     user.UpdateEmail(command.Email);
        // }

        if (int.Parse(command.Sex) != (int?)user.Gender)
        {
            if (!flag) flag = true;
            user.UpdateGender((Gender?)(int.Parse(command.Sex)));
        }

        if (command.Address != null && command.Address != user.Address)
        {
            if (!flag) flag = true;
            user.UpdateAddress(command.Address);
        }

        if (command.Avatar != null && !ByteArraysEqual(command.Avatar, user.Avatar))
        {
            if (!flag) flag = true;
            user.UpdateAvatar(command.Avatar);
        }

        if (command.PhoneNumber != null && command.PhoneNumber != user.PhoneNumber)
        {
            if (!flag) flag = true;
            user.UpdatePhoneNumber(command.PhoneNumber);
        }

        if (!flag)
            return Errors.User.NoUserInfoUpdate;

        // Save the updated admin profile to the repository
        await _adminRepository.UpdateAdminProfile(user);

        // You can return a success message or any other information if needed
        return new MessageResult("Admin profile updated successfully");
    }

    // The ByteArraysEqual function from the previous example
    private static bool ByteArraysEqual(byte[]? a1, byte[]? a2)
    {
        if (ReferenceEquals(a1, a2))
            return true;

        if (a1 is null || a2 is null || a1.Length != a2.Length)
            return false;

        for (int i = 0; i < a1.Length; i++)
        {
            if (a1[i] != a2[i])
                return false;
        }

        return true;
    }

    private static bool AllPropertiesNull(UpdateAdminProfileCommand command)
    {
        return command.FirstName == null &&
               command.LastName == null &&
               //    command.Email == null &&
               command.Address == null &&
               command.Avatar == null &&
               command.PhoneNumber == null;
    }
}
