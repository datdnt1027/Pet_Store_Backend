using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Admin.Commands;

public record UpdateRoleStatusCommand(
    string UserRoleId,
    bool Status = false
) : IRequest<ErrorOr<MessageResult>>;

public class UpdateRoleStatusCommandValidator : AbstractValidator<UpdateRoleStatusCommand>
{
    public UpdateRoleStatusCommandValidator()
    {
        RuleFor(command => command.UserRoleId)
            .NotEmpty().WithMessage("UserRoleId is required.")
            .Must(userRoleId => Guid.TryParse(userRoleId, out _)).WithMessage("Invalid UserRoleId format.");
    }
}



public class UpdateRoleStatusCommandHanlder : IRequestHandler<UpdateRoleStatusCommand, ErrorOr<MessageResult>>
{
    private readonly IAdminRepository _adminRepository;

    public UpdateRoleStatusCommandHanlder(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<ErrorOr<MessageResult>> Handle(UpdateRoleStatusCommand request, CancellationToken cancellationToken)
    {
        if (await _adminRepository.GetUserRoleFromId(Guid.Parse(request.UserRoleId)) is not UserRole userRole)
        {
            return Errors.UserRole.UserRoleNotExist;
        }

        userRole.UpdateStatus(request.Status);
        await _adminRepository.UpdateUserRole(userRole);
        return new MessageResult("Update Status UserRole Success");
    }
}