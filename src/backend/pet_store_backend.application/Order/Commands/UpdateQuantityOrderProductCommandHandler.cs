using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders;

namespace pet_store_backend.application.Order.Commands;


public record UpdateOrderProductQuantityCommand(
    string OrderProductId,
    int Quantity) : IRequest<ErrorOr<MessageResult>>;

public class UpdateOrderProductQuantityCommandValidator : AbstractValidator<UpdateOrderProductQuantityCommand>
{
    public UpdateOrderProductQuantityCommandValidator()
    {
        RuleFor(command => command.OrderProductId)
            .NotEmpty().WithMessage("Product ID cannot be empty.")
            .Must(BeValidGuid).WithMessage("Invalid format for Product ID.");

        RuleFor(command => command.Quantity)
            .Must(BeValidQuantity).WithMessage("Quantity must be a valid positive integer.");
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }

    private bool BeValidQuantity(int quantity)
    {
        return quantity > 0;
    }
}


public class UpdateQuantityOrderProductCommandHandler : IRequestHandler<UpdateOrderProductQuantityCommand, ErrorOr<MessageResult>>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateQuantityOrderProductCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<MessageResult>> Handle(UpdateOrderProductQuantityCommand request, CancellationToken cancellationToken)
    {
        if (await _orderRepository.RetrieveOrderProduct(Guid.Parse(request.OrderProductId)) is not OrderProduct orderProduct)
        {
            return Errors.Order.NoOrderProductPayment;
        }

        if (orderProduct.Quantity == request.Quantity)
        {
            return Errors.Order.NoQuantityOrderProductUpdate;
        }
        orderProduct.UpdateQuantityOrderProduct(request.Quantity);
        await _orderRepository.UpdateOrderProduct(orderProduct);

        return new MessageResult("Update Order Product Quantity Success !");
    }
}