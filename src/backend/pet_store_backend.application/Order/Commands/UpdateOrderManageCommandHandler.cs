using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.application.Order.Commands;


public record UpdateOrderManageCommand(
    string OrderId,
    string OrderStatus,
    string ExpectedDeliveryStartDate,
    string ExpectedDeliveryEndDate
) : IRequest<ErrorOr<MessageResult>>;

public class UpdateOrderManageCommandValidator : AbstractValidator<UpdateOrderManageCommand>
{
    public UpdateOrderManageCommandValidator()
    {
        RuleFor(command => command.OrderId)
        .NotEmpty().WithMessage("OrderId is required.")
        .Must(BeValidGuid).WithMessage("Invalid format for Order ID.");

        RuleFor(command => command.OrderStatus)
            .NotEmpty().WithMessage("OrderStatus is required.")
            .IsEnumName(typeof(OrderStatus)).WithMessage("Invalid OrderStatus value.");

        RuleFor(command => command.ExpectedDeliveryStartDate)
            .NotEmpty().WithMessage("ExpectedDeliveryStartDate is required.")
            .Must(BeValidDate).WithMessage("ExpectedDeliveryStartDate must be a valid date.")
            .When(command => command.OrderStatus == OrderStatus.Deliver.ToString());

        RuleFor(command => command.ExpectedDeliveryEndDate)
            .NotEmpty().WithMessage("ExpectedDeliveryEndDate is required.")
            .Must(BeValidDate).WithMessage("ExpectedDeliveryEndDate must be a valid date.")
            .When(command => command.OrderStatus == OrderStatus.Deliver.ToString());
    }

    private bool BeValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}

public class UpdateOrderManageCommandHandler : IRequestHandler<UpdateOrderManageCommand, ErrorOr<MessageResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateOrderManageCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
    {
        _orderRepository = orderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<MessageResult>> Handle(UpdateOrderManageCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Errors.User.UserNotSignIn;
        }

        if (await _orderRepository.RetrieveOrder(Guid.Parse(request.OrderId)) is not pet_store_backend.domain.Entities.Orders.Order order)
        {
            return Errors.Order.NoOrderExist;
        }

        if (request.OrderStatus != order.OrderStatus.ToString())
        {
            var orderStatus = Enum.Parse<OrderStatus>(request.OrderStatus);
            order.UpdateOrderStatus(orderStatus);
            if (orderStatus == OrderStatus.Deliver)
            {
                order.UpdateOrderDelivery(DateTime.Parse(request.ExpectedDeliveryStartDate), DateTime.Parse(request.ExpectedDeliveryEndDate));
            }
            order.UpdateUserId(UserId.Create(Guid.Parse(userId)));
            await _orderRepository.UpdateOrder(order);
            return new MessageResult("Order Update Successfully");
        }
        return Errors.Order.NoOrderInfoUpdate;
    }
}
