using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Order.Commands;
using pet_store_backend.contracts;
using pet_store_backend.contracts.Order;
using pet_store_backend.contracts.Payment;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public OrderController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("customer")]
    [Authorize(Roles = UserRoleKey.UserRoleName)]
    public async Task<IActionResult> CustomerOrderProduct(OrderProductRequest request)
    {
        var command = _mapper.Map<OrderProductCommand>(request);
        var createOrderProduct = await _mediator.Send(command);

        return createOrderProduct.Match(orderProduct => Ok(_mapper.Map<MessageResponse>(orderProduct)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("quantity/customer")]
    [Authorize(Roles = UserRoleKey.UserRoleName)]
    public async Task<IActionResult> UpdateQuantCustomerOrderProduct(UpdateOrderProductQuantityRequest request)
    {
        var command = _mapper.Map<UpdateOrderProductQuantityCommand>(request);
        var updateQuantityOrderProduct = await _mediator.Send(command);

        return updateQuantityOrderProduct.Match(orderProduct => Ok(_mapper.Map<MessageResponse>(orderProduct)),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("customer")]
    [Authorize(Roles = UserRoleKey.UserRoleName)]
    public async Task<IActionResult> CustomerGetOrderProduct()
    {
        GetOrderProductCommand command = new();
        var getOrderProduct = await _mediator.Send(command);

        return getOrderProduct.Match(orderProduct => Ok(_mapper.Map<OrderProductResponse>(orderProduct)),
            errors => Problem(errors));
    }

    [HttpDelete]
    [Route("delete/{orderProductId}")]
    [Authorize(Roles = UserRoleKey.UserRoleName)]
    public async Task<IActionResult> CustomerDeleteProduct(string orderProductId)
    {
        var command = new DeleteOrderProductCommand(orderProductId);
        var createOrderProduct = await _mediator.Send(command);

        return createOrderProduct.Match(orderProduct => Ok(_mapper.Map<MessageResponse>(orderProduct)),
            errors => Problem(errors));
    }



    [HttpPost]
    [Route("payment/momo")]
    [Authorize(Roles = UserRoleKey.UserRoleName)]
    public async Task<IActionResult> ProductsPayment(MomoOneTimePaymentRequest request)
    {
        var command = _mapper.Map<MomoOneTimePaymentProductCommand>(request);
        var paymentMessage = await _mediator.Send(command);

        return paymentMessage.Match(payment => Ok(_mapper.Map<MomoOneTimePaymentResponse>(payment)),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("payment/momo-return")]
    [AllowAnonymous]
    // [Authorize(Roles = UserRoleKey.UserRoleName)]
    public async Task<IActionResult> ProductsPaymentReturn([FromQuery] MomoPaymentProductReturnRequest request)
    {
        var command = _mapper.Map<MomoPaymentProductReturnCommand>(request);
        var paymentMessage = await _mediator.Send(command);

        return paymentMessage.Match(payment => Ok(_mapper.Map<MomoPaymentReturnResponse>(payment)),
            errors => Problem(errors)
        );
    }

}