using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;

namespace pet_store_backend.application.Order.Queries;

public record OrderByBatchQuery(

) : IRequest<ErrorOr<List<OrderManageResult>>>;

public class OrderByBatchQueryValidator : AbstractValidator<OrderByBatchQuery>
{
    public OrderByBatchQueryValidator()
    {

    }
}

public class GetOrderCommandHandler : IRequestHandler<OrderByBatchQuery, ErrorOr<List<OrderManageResult>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<List<OrderManageResult>>> Handle(OrderByBatchQuery request, CancellationToken cancellationToken)
    {
        var listOrder = await _orderRepository.GetListOrderManage();
        return listOrder;
    }
}