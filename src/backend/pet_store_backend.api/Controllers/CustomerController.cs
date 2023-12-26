using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Customer.Commands;
using pet_store_backend.application.Customer.Queries;
using pet_store_backend.application.Order.Queries;
using pet_store_backend.contracts;
using pet_store_backend.contracts.Order;
using pet_store_backend.contracts.User;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = UserRoleKey.UserRoleName)]
public class CustomerController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public CustomerController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var query = new UserProfileQuery();
        var userInfo = await _mediator.Send(query);

        return userInfo.Match(user => Ok(_mapper.Map<UserProfileResponse>(user)),
            errors => Problem(errors));
    }

    [HttpPatch]
    [Route("update_profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
    {
        var command = _mapper.Map<UpdateUserProfileCommand>(request);
        var updateProfile = await _mediator.Send(command);

        return updateProfile.Match(profile => Ok(_mapper.Map<MessageResponse>(profile)),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("orders")]
    public async Task<IActionResult> RetrieveCustomerOrderHistory([FromQuery] int page = 1)
    {
        var query = new OrderProductByBatchQuery(page);
        var orderHistoryByBatch = await _mediator.Send(query);

        return orderHistoryByBatch.Match(order => Ok(_mapper.Map<List<OrderResponse>>(order)),
            errors => Problem(errors));

    }

}