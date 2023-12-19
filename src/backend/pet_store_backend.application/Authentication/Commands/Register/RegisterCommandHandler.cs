using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Utils;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities;

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

    public async Task<ErrorOr<MessageResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Validate User doesn't exist
        if (await _userRepository.GetCustomerByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        _passwordConfiguration.CreatePasswordHash(command.Password, out byte[] passwordHash, out byte[] passwordSalt);
        // Create user (generate unique Id) & Persist to DB
        var customer = pet_store_backend.domain.Entities.Users.Customer.Create
        (
            command.FirstName,
            command.LastName,
            command.Email,
            passwordHash,
            passwordSalt,
            null!
        );
        customer.CreateVerificationToken(_passwordConfiguration.CreateRandomToken(), DateTime.Now.AddMinutes(HttpContextItemKeys.ExpireTokenInMinutes));
        await _userRepository.Add(customer);

        var message = new Message(new string[] {
            customer.Email },
            "Pet Store Verfication Email",
            $"Your verfication link {HttpContextItemKeys.UrlFrontEndRegisterToken}/{customer.VerificationToken}");
        _emailService.SendEmail(message);

        return new MessageResult("User successfully created. Please verfication !");
    }
}