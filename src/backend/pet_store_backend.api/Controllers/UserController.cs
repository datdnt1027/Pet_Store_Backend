using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Admin.Commands;
using pet_store_backend.application.Admin.Queries;
using pet_store_backend.contracts;
using pet_store_backend.contracts.User;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = UserRoleKey.UserRoleName)]
public class UserController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public UserController(ISender mediator, IMapper mapper)
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

}