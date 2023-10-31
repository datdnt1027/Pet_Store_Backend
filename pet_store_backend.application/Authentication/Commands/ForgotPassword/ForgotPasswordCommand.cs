using ErrorOr;
using MediatR;
using pet_store_backend.application.Authentication.Common;

namespace pet_store_backend.application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(
    string Email) : IRequest<ErrorOr<MessageResult>>;