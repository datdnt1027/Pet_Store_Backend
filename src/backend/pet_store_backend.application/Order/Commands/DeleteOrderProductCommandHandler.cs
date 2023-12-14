using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders;

namespace pet_store_backend.application.Order.Commands;

public record DeleteOrderProductCommand(
    string OrderProductId
) : IRequest<ErrorOr<MessageResult>>;

public class DeleteOrderProductCommandValidator : AbstractValidator<DeleteOrderProductCommand>
{
    public DeleteOrderProductCommandValidator()
    {
        RuleFor(command => command.OrderProductId)
            .NotEmpty().WithMessage("Order Product ID cannot be empty.")
            .Must(BeValidGuid).WithMessage("Invalid format for Order Product ID.");
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}
public class DeleteOrderProductCommandHandler : IRequestHandler<DeleteOrderProductCommand, ErrorOr<MessageResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteOrderProductCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
    {
        _orderRepository = orderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<MessageResult>> Handle(DeleteOrderProductCommand request, CancellationToken cancellationToken)
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId == null)
        {
            return Errors.User.UserNotSignIn;
        }
        if ((await _orderRepository.CheckOrderIsExist(Guid.Parse(request.OrderProductId), Guid.Parse(customerId))) is OrderProduct orderProduct)
        {
            await _orderRepository.DeleteOrderProduct(orderProduct);
        }
        else
        {
            return Errors.Order.NoOrderProductDelete;
        }

        return new MessageResult("Delete Order Product success !");
    }
}