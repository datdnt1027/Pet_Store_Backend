using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;

namespace pet_store_backend.application.Authentication.Commands.Verify;

public record VerifyCommand(
    string VerificationToken) : IRequest<ErrorOr<MessageResult>>;