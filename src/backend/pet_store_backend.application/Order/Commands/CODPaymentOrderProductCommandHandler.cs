using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders.ValueObjects;

namespace pet_store_backend.application.Order.Commands;


public record CODPaymentOrderProductCommand(

) : IRequest<ErrorOr<CODPaymentResult>>;

public class CODPaymentOrderProductCommandValidator : AbstractValidator<CODPaymentOrderProductCommand>
{
    public CODPaymentOrderProductCommandValidator()
    {
    }
}

public class CODPaymentOrderProductCommandHandler : IRequestHandler<CODPaymentOrderProductCommand, ErrorOr<CODPaymentResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CODPaymentOrderProductCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
    {
        _orderRepository = orderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<CODPaymentResult>> Handle(CODPaymentOrderProductCommand request, CancellationToken cancellationToken)
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId == null)
        {
            return Errors.User.UserNotExist;
        }
        var orderId = OrderId.CreateUnique().Value;
        var ProductsPayment = await _orderRepository.RetrieveOrderedProductsForCustomer(Guid.Parse(customerId));
        if (ProductsPayment != null)
        {
            var totalPriceProductPayment = ProductsPayment.Sum(op => op.Quantity * op.Product.ProductPrice);
            await _orderRepository.UpdateProductPaymentCODInCart(Guid.Parse(customerId), orderId);
            return new CODPaymentResult(
                orderId.ToString(),
                "Payment COD Success",
                DateTime.Now.ToString(),
                (long)totalPriceProductPayment
            );
        }
        return Errors.Order.NoOrderProductPayment;
    }
}