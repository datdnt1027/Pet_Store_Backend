using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderProductCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
    {
        _orderRepository = orderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<OrderProductResult>> Handle(GetOrderProductCommand request, CancellationToken cancellationToken)
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId == null)
        {
            return Errors.User.UserNotExist;
        }
        var orderProducts = await _orderRepository.RetrieveOrderedProductsForCustomer(Guid.Parse(customerId));
        if (orderProducts != null)
        {
            var totalQuantityProduct = orderProducts.Sum(op => op.Quantity);
            var totalPrice = orderProducts.Sum(op => op.Quantity * op.Product.ProductPrice);

            var orderProductResult = new OrderProductResult(
                totalQuantityProduct,
                (long)totalPrice,
                orderProducts
            );

            return orderProductResult;
        }
        return Errors.Order.NoOrderProductPayment;
    }
}