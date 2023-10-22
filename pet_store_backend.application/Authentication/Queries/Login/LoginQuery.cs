using ErrorOr;
using MediatR;
using pet_store_backend.application.Services.Authentication;

namespace pet_store_backend.application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;