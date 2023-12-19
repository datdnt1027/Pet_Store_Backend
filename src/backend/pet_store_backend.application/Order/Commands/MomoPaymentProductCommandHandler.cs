using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders.ValueObjects;

namespace pet_store_backend.application.Order.Commands;

public record MomoOneTimePaymentProductCommand(
// string RedirectUrl
) : IRequest<ErrorOr<PaymentResponse>>;

public class MomoOneTimePaymentProductCommandValidator : AbstractValidator<MomoOneTimePaymentProductCommand>
{
    public MomoOneTimePaymentProductCommandValidator()
    {
        // RuleFor(command => command.RedirectUrl)
        //     .Must(url => string.IsNullOrEmpty(url) || BeAValidUrl(url))
        //     .WithMessage("If provided, RedirectUrl must be a valid URL.");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

public class MomoPaymentProductCommandHandler : IRequestHandler<MomoOneTimePaymentProductCommand, ErrorOr<PaymentResponse>>
{
    private readonly IPaymentProvider _paymentProvider;
    private readonly IOrderRepository _orderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MomoPaymentProductCommandHandler(IPaymentProvider paymentProvider, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
    {
        _paymentProvider = paymentProvider;
        _orderRepository = orderRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ErrorOr<PaymentResponse>> Handle(MomoOneTimePaymentProductCommand request, CancellationToken cancellationToken)
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

            (bool createMomoLinkResult, string? message) = _paymentProvider
                .GetLinkPaymentMomo(
                    orderId.ToString(),
                    (long)(totalPriceProductPayment),
                    orderId.ToString(),
                    "Pet Store Payment");

            if (createMomoLinkResult && message != null)
            {
                var responseData = JsonConvert.DeserializeObject<PaymentResponse>(message);
                if (responseData?.ResultCode == "0")
                {
                    await _orderRepository.UpdateProductPaymentInCart(Guid.Parse(customerId), orderId);
                    return responseData;
                }
                else
                {
                    Errors.Payment.PaymentProblem(responseData?.Message ?? "Payment Failure!");
                }
            }
            else
            {
                Errors.Payment.PaymentProblem(message ?? "Payment Failure!");
            }
        }
        return Errors.Order.NoOrderProductPayment;
    }
}