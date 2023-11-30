using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Utils;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities;
using pet_store_backend.domain.Entities.User;
using pet_store_backend.domain.Entities.User.ValueObjects;

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
        if (await _userRepository.GetUserByVerificationToken(query.VerificationToken) is not User user)
        {
            return Errors.Authentication.InvalidToken;
        }
        else if (user.TokenExpires < DateTime.Now)
        {
            user.CreateVerificationToken(_passwordConfiguration.CreateRandomToken(), DateTime.Now.AddMinutes(HttpContextItemKeys.ExpireTokenInMinutes));
            await _userRepository.Update(user);
            var message = new Message(new string[] {
                user.Email },
                "Pet Store Verfication Email",
                $"Your verfication link {HttpContextItemKeys.UrlFrontEndRegisterToken}/{user.VerificationToken}");
            _emailService.SendEmail(message);

            return Errors.Authentication.TokenExpire;
        }
        if (await _userRepository.GetGuestRoleId() is not UserRoleId userRoleId)
        {
            return Errors.Authentication.ForbidenPermission;
        }

        user.UpdateVerifiedAt(DateTime.Now); // If update verified date the VerficationToken is gone
        user.UpdateUserRoleId(userRoleId);

        await _userRepository.Update(user);

        return new MessageResult(Message: "User Verified");
    }
}