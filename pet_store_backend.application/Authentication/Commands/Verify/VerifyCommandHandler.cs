using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Authentication.Commands.Verify;

public class VerifyQueryHandler : IRequestHandler<VerifyCommand, ErrorOr<MessageResult>>
{
    private readonly IUserRepository _userRepository;

    public VerifyQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
            return Errors.Authentication.TokenExpire;
        }

        user.UpdateVerifiedAt(DateTime.Now); // If update verified date the VerficationToken is gone
        await _userRepository.Update(user);

        return new MessageResult(Message: "User Verified");
    }
}