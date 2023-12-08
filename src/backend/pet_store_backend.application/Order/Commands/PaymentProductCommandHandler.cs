using ErrorOr;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Order.Commands;

public record MomoOneTimePaymentProductCommand(
    string RedirectUrl
) : IRequest<ErrorOr<PaymentResponse>>;

public class MomoOneTimePaymentProductCommandValidator : AbstractValidator<MomoOneTimePaymentProductCommand>
{
    public MomoOneTimePaymentProductCommandValidator()
    {
        RuleFor(command => command.RedirectUrl)
            .Must(url => string.IsNullOrEmpty(url) || BeAValidUrl(url))
            .WithMessage("If provided, RedirectUrl must be a valid URL.");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

public class PaymentProductCommandHandler : IRequestHandler<MomoOneTimePaymentProductCommand, ErrorOr<PaymentResponse>>
{
    private readonly IPaymentProvider _paymentProvider;
    private readonly IOrderRepository _orderRepository;
    public PaymentProductCommandHandler(IPaymentProvider paymentProvider, IOrderRepository orderRepository)
    {
        _paymentProvider = paymentProvider;
        _orderRepository = orderRepository;
    }
    public async Task<ErrorOr<PaymentResponse>> Handle(MomoOneTimePaymentProductCommand request, CancellationToken cancellationToken)
    {
        var order = pet_store_backend.domain.Entities.Orders.Order.CreateOrder();
        await _orderRepository.AddOrder(order);
        var ProductsPayment = await _orderRepository.RetrieveTotalOrderProductPaymentInCart(order.Id);
        if (ProductsPayment != null)
        {
            var totalPriceProductPayment = ProductsPayment.Sum(op => op.OrderProduct.Quantity * op.Price);

            (bool createMomoLinkResult, string? message) = _paymentProvider.GetLinkPaymentMomo(order.Id.Value.ToString(), (long)(totalPriceProductPayment), order.Id.Value.ToString(), "Pet Store Payment", request.RedirectUrl);

            if (createMomoLinkResult && message != null)
            {
                var responseData = JsonConvert.DeserializeObject<PaymentResponse>(message);
                if (responseData?.ResultCode == "0")
                {
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