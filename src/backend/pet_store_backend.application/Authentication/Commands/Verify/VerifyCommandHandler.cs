using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Utils;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities;
using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Authentication.Commands.Verify;

public class VerifyQueryHandler : IRequestHandler<VerifyCommand, ErrorOr<MessageResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordConfiguration _passwordConfiguration;

    public VerifyQueryHandler(IUserRepository userRepository, IEmailService emailService, IPasswordConfiguration passwordConfiguration)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordConfiguration = passwordConfiguration;
    }

    public async Task<ErrorOr<MessageResult>> Handle(VerifyCommand query, CancellationToken cancellationToken)
    {
        // Check if user registered
        if (await _userRepository.GetCustomerByVerificationToken(query.VerificationToken) is not pet_store_backend.domain.Entities.Users.Customer customer)
        {
            return Errors.Authentication.InvalidToken;
        }
        else if (customer.TokenExpires < DateTime.Now)
        {
            customer.CreateVerificationToken(_passwordConfiguration.CreateRandomToken(), DateTime.Now.AddMinutes(HttpContextItemKeys.ExpireTokenInMinutes));
            await _userRepository.Update(customer);
            var message = new Message(new string[] {
                customer.Email },
                "Pet Store Verfication Email",
                $"Your verfication link {HttpContextItemKeys.UrlFrontEndRegisterToken}/{customer.VerificationToken}");
            _emailService.SendEmail(message);

            return Errors.Authentication.TokenExpire;
        }
        if (await _userRepository.GetGuestRoleId() is not UserRoleId userRoleId)
        {
            return Errors.Authentication.ForbidenPermission;
        }

        customer.UpdateVerifiedAt(DateTime.Now); // If update verified date the VerficationToken is gone
        customer.UpdateUserRoleId(userRoleId);

        await _userRepository.Update(customer);

        return new MessageResult(Message: "User Verified");
    }
}