using ErrorOr;
using MediatR;
using pet_store_backend.application.Services.Authentication;

namespace pet_store_backend.application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;