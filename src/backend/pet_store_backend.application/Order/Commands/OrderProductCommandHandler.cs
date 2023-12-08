using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders;

namespace pet_store_backend.application.Order.Commands;

public record OrderProductCommand(
    string ProductId,
    int Quantity) : IRequest<ErrorOr<MessageResult>>;

public class OrderCommandValidator : AbstractValidator<OrderProductCommand>
{
    public OrderCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty().WithMessage("Product ID cannot be empty.")
            .Must(BeValidGuid).WithMessage("Invalid format for Product ID.");

        RuleFor(command => command.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}
public class OrderProductCommandHandler : IRequestHandler<OrderProductCommand, ErrorOr<MessageResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICollectionRepository _collectionRepository;

    public OrderProductCommandHandler(IOrderRepository orderRepository, ICollectionRepository collectionRepository)
    {
        _orderRepository = orderRepository;
        _collectionRepository = collectionRepository;
    }

    public async Task<ErrorOr<MessageResult>> Handle(OrderProductCommand request, CancellationToken cancellationToken)
    {
        if (!(await _collectionRepository.CheckProductIsValid(Guid.Parse(request.ProductId))))
        {
            return Errors.Product.NullProduct;
        }
        if ((await _orderRepository.CheckOrderIsExist(Guid.Parse(request.ProductId))) is OrderProduct orderProduct)
        {
            int updateQuantity = request.Quantity + orderProduct.Quantity;
            orderProduct.UpdateQuantityOrderProduct(updateQuantity);
            await _orderRepository.UpdateOrderProduct(orderProduct);
        }
        else
        {
            if (!await _orderRepository.AddProductOrder(Guid.Parse(request.ProductId), request.Quantity))
            {
                return Errors.Order.OrderProductAddProblem;
            }
        }

        return new MessageResult("Add to Cart success !");
    }
}