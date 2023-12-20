using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Order.Queries;

public record OrderProductByBatchQuery(
    int Page = 1
) : IRequest<ErrorOr<List<OrderResult>>>;

public class OrderHistoryByBatchQueryValidator : AbstractValidator<OrderProductByBatchQuery>
{
    public OrderHistoryByBatchQueryValidator()
    {
        RuleFor(x => x.Page)
            .Must(page =>
            {
                if (!int.TryParse(page.ToString(), out _))
                {
                    return false;
                }

                return true;
            })
            .GreaterThan(0)
            .WithMessage("Page must be a valid integer and greater than 0");
    }
}

public class GetOrderProductHistoryCommandHandler : IRequestHandler<OrderProductByBatchQuery, ErrorOr<List<OrderResult>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderProductHistoryCommandHandler(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<List<OrderResult>>> Handle(OrderProductByBatchQuery query, CancellationToken cancellationToken)
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId == null)
        {
            return Errors.User.UserNotExist;
        }
        var orderHistoryByBatch = await _orderRepository.RetrieveOrderHistory(Guid.Parse(customerId), query.Page);

        if (orderHistoryByBatch == null)
        {
            // Return an appropriate response for no products found
            return new List<OrderResult>(); // or any other default response
        }

        return orderHistoryByBatch;
    }
}
