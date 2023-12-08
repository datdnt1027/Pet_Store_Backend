using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Admin.Commands;
using pet_store_backend.application.Admin.Queries;
using pet_store_backend.application.Authentication.Queries.Login;
using pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;
using pet_store_backend.contracts;
using pet_store_backend.contracts.Admin;
using pet_store_backend.contracts.Authentication;
using pet_store_backend.contracts.PetProducts;
using pet_store_backend.infrastructure.Authentication;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = UserRoleKey.AdminRoleName)]
public class AdminController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AdminController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQueryUser>(request);

        var authResult = await _mediator.Send(query);

        return authResult.Match(authResult => Ok(_mapper.Map<AdminResponse>(authResult)),
            errors => Problem(errors));
    }


    [HttpGet("roles")]
    [HasPermission(TableKey.UserRoles, PermissionType.Read)]
    public async Task<IActionResult> GetUserRoles()
    {
        var query = new RetriveUserRole();
        var retriveUserRoles = await _mediator.Send(query);
        return retriveUserRoles.Match(
            userRoles => Ok(_mapper.Map<IEnumerable<UserRoleResponse>>(userRoles)),
            errors => Problem(errors)
        );
    }

    [HttpPost("collections")]
    [HasPermission(TableKey.Categories, PermissionType.Create)]
    [HasPermission(TableKey.Products, PermissionType.Create)]
    public async Task<IActionResult> CreateCatory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var createCategoryRequest = await _mediator.Send(command);

        return createCategoryRequest.Match(category => Ok(_mapper.Map<MessageResponse>(category)),
            errors => Problem(errors));
    }

    [HttpPatch]
    [Route("products/{productId}")]
    [HasPermission(TableKey.Products, PermissionType.Update)]
    public async Task<IActionResult> UpdateStatusProduct(string productId, UpdateProductRequest request)
    {
        var command = _mapper.Map<UpdateProductCommand>(request);
        var updateProduct = await _mediator.Send(command);

        return updateProduct.Match(product => Ok(_mapper.Map<MessageResponse>(product)),
            errors => Problem(errors));
    }
}