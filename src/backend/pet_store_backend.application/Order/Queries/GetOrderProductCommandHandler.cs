using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Order.Commands;

public record GetOrderProductCommand(
) : IRequest<ErrorOr<OrderProductResult>>;

public class GetOrderCommandValidator : AbstractValidator<GetOrderProductCommand>
{
    public GetOrderCommandValidator()
    {
    }
}
public class GetOrderProductCommandHandler : IRequestHandler<GetOrderProductCommand, ErrorOr<OrderProductResult>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderProductCommandHandler(IOrderRepository orderRepository, ICollectionRepository collectionRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<OrderProductResult>> Handle(GetOrderProductCommand request, CancellationToken cancellationToken)
    {
        var orderProducts = await _orderRepository.RetrieveOrderedProductsForUser();
        if (orderProducts != null)
        {
            var totalQuantityProduct = orderProducts.Sum(op => op.Quantity);
            var totalPrice = orderProducts.Sum(op => op.Quantity * op.Product.ProductPrice.Value);

            var productResults = orderProducts.Select(op => new ProductResultOrder
            (
                op.Product.ProductName,
                op.Product.ProductDetail,
                op.Product.ProductPrice.Value,
                op.Product.ImageData ?? Array.Empty<byte>(),
                op.Quantity
            )).ToList();

            var orderProductResult = new OrderProductResult(
                totalQuantityProduct,
                (long)totalPrice,
                productResults
            );

            return orderProductResult;
        }
        return Errors.Order.NoOrderProductPayment;
    }
}