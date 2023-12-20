using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Order.Commands;

public record MomoPaymentProductReturnCommand(
    string RequestId,
    string OrderId,
    long Amount,
    long ResponseTime,
    string OrderInfo,
    string OrderType,
    string TransId,
    string Message,
    int ResultCode,
    string PayType,
    string ExtraData,
    string Signature
) : IRequest<ErrorOr<MomoPaymentReturnResult>>;

public class MomoPaymentProductReturnCommandValidator : AbstractValidator<MomoPaymentProductReturnCommand>
{
    public MomoPaymentProductReturnCommandValidator()
    {

    }
}
public class MomoPaymentProductReturnCommandHandler : IRequestHandler<MomoPaymentProductReturnCommand, ErrorOr<MomoPaymentReturnResult>>
{
    private readonly IPaymentProvider _paymentProvider;
    private readonly IOrderRepository _orderRepository;
    public MomoPaymentProductReturnCommandHandler(IPaymentProvider paymentProvider, IOrderRepository orderRepository)
    {
        _paymentProvider = paymentProvider;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<MomoPaymentReturnResult>> Handle(MomoPaymentProductReturnCommand request, CancellationToken cancellationToken)
    {

        var isValidSignature = _paymentProvider.IsValidSignature(
            request.RequestId,
            request.OrderId,
            request.Amount,
            request.ResponseTime,
            request.OrderInfo,
            request.OrderType,
            request.TransId,
            request.Message,
            request.ResultCode,
            request.PayType,
            request.ExtraData,
            request.Signature);
        if (isValidSignature)
        {
            if (request.ResultCode == 0)
            {
                await _orderRepository.UpdateOrderE_WalletStatusAccept(Guid.Parse(request.OrderId));
                var result = new MomoPaymentReturnResult(
                    request.OrderId,
                    "Payment Momo Success",
                    DateTime.Now.ToString(),
                    request.Amount,
                    request.Signature
                );

                return result;
            }
            else
            {
                return Errors.Payment.PaymentProblem("Payment process failed");
            }
        }
        else
        {
            return Errors.Payment.PaymentProblem("Invalid signature in response");
        }
    }
}