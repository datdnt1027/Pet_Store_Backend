using System.Security.Cryptography;
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

namespace pet_store_backend.application.Authentication.Commands.Register;

public class RegisterCommadHandler : IRequestHandler<RegisterCommand, ErrorOr<MessageResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordConfiguration _passwordConfiguration;
    private readonly IEmailService _emailService;

    public RegisterCommadHandler(IUserRepository userRepository, IPasswordConfiguration passwordConfiguration, IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordConfiguration = passwordConfiguration;
        _emailService = emailService;
    }

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<ErrorOr<MessageResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Validate User doesn't exist
        if (await _userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        _passwordConfiguration.CreatePasswordHash(command.Password, out byte[] passwordHash, out byte[] passwordSalt);
        // Create user (generate unique Id) & Persist to DB
        var user = User.Create
        (
            command.FirstName,
            command.LastName,
            command.Email,
            passwordHash,
            passwordSalt
        );
        user.CreateVerificationToken(CreateRandomToken());
        await _userRepository.Add(user);
        var message = new Message(new string[] {
            user.Email },
            "Pet Store Verfication Email",
            $"Your verfication link {HttpContextItemKeys.UrlFrontEndRegisterToken}/{user.VerificationToken}");
        _emailService.SendEmail(message);

        return new MessageResult("User successfully created. Please verfication !");
    }
}