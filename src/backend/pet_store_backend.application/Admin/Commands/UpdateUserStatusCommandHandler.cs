using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Admin.Commands;

public record UpdateCustomerStatusCommand(
    string CustomerId,
    string Status
) : IRequest<ErrorOr<MessageResult>>;

public class UpdateCustomerStatusCommandValidator : AbstractValidator<UpdateCustomerStatusCommand>
{
    public UpdateCustomerStatusCommandValidator()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.")
            .Must(customerId => Guid.TryParse(customerId, out _)).WithMessage("Invalid CustomerId format.");

        RuleFor(command => command.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => status == "0" || status == "1").WithMessage("Invalid Status format.");
    }
}


public class UpdateCustomerStatusCommandHanlder : IRequestHandler<UpdateCustomerStatusCommand, ErrorOr<MessageResult>>
{
    private readonly IUserRepository _userRepository;

    public UpdateCustomerStatusCommandHanlder(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private static bool ConvertToBool(string status)
    {
        return status == "1";
    }

    public async Task<ErrorOr<MessageResult>> Handle(UpdateCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.RetrieveUser(Guid.Parse(request.CustomerId)) is not pet_store_backend.domain.Entities.Users.Customer customer)
        {
            return Errors.User.UserNotExist;
        }

        if (customer.Status == ConvertToBool(request.Status))
        {
            return Errors.User.NoUserInfoUpdate;
        }

        customer.UpdateStatus(ConvertToBool(request.Status));

        await _userRepository.UpdateCustomerStatusAsync(customer);
        return new MessageResult("Update Status Customer Success");
    }
}
