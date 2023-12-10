using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;

namespace pet_store_backend.application.Admin.Commands;

public record UpdateRoleStatusCommand(
    bool Status
) : IRequest<ErrorOr<MessageResult>>;