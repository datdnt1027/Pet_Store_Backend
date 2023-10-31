using ErrorOr;
using MediatR;
using pet_store_backend.application.Authentication.Common;

namespace pet_store_backend.application.Authentication.Commands.Verify;

public record VerifyCommand(
    string VerficationToken) : IRequest<ErrorOr<MessageResult>>;