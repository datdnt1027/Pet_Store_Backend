using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Orders;

namespace pet_store_backend.application.Order.Commands;

public record GetOrderProductCommand(
) : IRequest<ErrorOr<OrderProductResult>>;

public class GetOrderCommandValidator : AbstractValidator<GetOrderProductCommand>
{
    public GetOrderCommandValidator()
    {
    }
}
// public class GetOrderProductCommandHandler : IRequestHandler<GetOrderProductCommand, ErrorOr<OrderProductResult>>
// {
//     private readonly IOrderRepository _orderRepository;
//     private readonly ICollectionRepository _collectionRepository;

//     public GetOrderProductCommandHandler(IOrderRepository orderRepository, ICollectionRepository collectionRepository)
//     {
//         _orderRepository = orderRepository;
//         _collectionRepository = collectionRepository;
//     }

//     public async Task<ErrorOr<OrderProductResult>> Handle(GetOrderProductCommand request, CancellationToken cancellationToken)
//     {
//         return null!;
//     }
// }