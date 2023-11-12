using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;

namespace pet_store_backend.application.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Token,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<MessageResult>>;