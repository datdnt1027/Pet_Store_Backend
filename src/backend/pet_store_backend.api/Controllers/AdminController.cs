using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Admin.Commands;
using pet_store_backend.application.Admin.Queries;
using pet_store_backend.application.Authentication.Queries.Login;
using pet_store_backend.application.Order.Commands;
using pet_store_backend.application.Order.Queries;
using pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;
using pet_store_backend.application.PetProducts.PetProduct.Commands;
using pet_store_backend.contracts;
using pet_store_backend.contracts.Admin;
using pet_store_backend.contracts.Authentication;
using pet_store_backend.contracts.Order;
using pet_store_backend.contracts.PetProducts;
using pet_store_backend.infrastructure.Authentication;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var query = new AdminProfileQuery();
        var authResult = await _mediator.Send(query);

        return authResult.Match(authResult => Ok(_mapper.Map<AdminProfileResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPatch]
    [Route("update_profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateAdminProfileRequest request)
    {
        var command = _mapper.Map<UpdateAdminProfileCommand>(request);
        var updateProfile = await _mediator.Send(command);

        return updateProfile.Match(profile => Ok(_mapper.Map<MessageResponse>(profile)),
            errors => Problem(errors));
    }


    [HttpGet("roles")]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
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

    [HttpPatch]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("role/status")]
    [HasPermission(TableKey.UserRoles, PermissionType.Deactivate)]
    public async Task<IActionResult> UpdateRoleStatus(UpdateRoleStatusRequest request)
    {
        var command = _mapper.Map<UpdateRoleStatusCommand>(request);
        var updateStatus = await _mediator.Send(command);
        return updateStatus.Match(
            status => Ok(_mapper.Map<MessageResponse>(status)),
            errors => Problem(errors)
        );
    }

    [HttpPost("collections")]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [HasPermission(TableKey.Categories, PermissionType.Create)]
    [HasPermission(TableKey.Products, PermissionType.Create)]
    public async Task<IActionResult> CreateCatory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var createCategoryRequest = await _mediator.Send(command);

        return createCategoryRequest.Match(category => Ok(_mapper.Map<MessageResponse>(category)),
            errors => Problem(errors));
    }

    [HttpPost("product")]
    // [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [HasPermission(TableKey.Products, PermissionType.Create)]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var createProductResult = await _mediator.Send(command);

        return createProductResult.Match(
            success => Ok(_mapper.Map<MessageResponse>(success)),
            errors => Problem(errors)
        );
    }

    [HttpPatch]
    [Route("product")]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [HasPermission(TableKey.Products, PermissionType.Update)]
    [HasPermission(TableKey.Products, PermissionType.Deactivate)]
    public async Task<IActionResult> UpdateStatusProduct(UpdateProductRequest request)
    {
        var command = _mapper.Map<UpdateProductCommand>(request);
        var updateProduct = await _mediator.Send(command);

        return updateProduct.Match(product => Ok(_mapper.Map<MessageResponse>(product)),
            errors => Problem(errors));
    }

    [HttpPatch]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("customer/status")]
    [HasPermission(TableKey.Customers, PermissionType.Deactivate)]
    public async Task<IActionResult> UpdateUserStatus(UpdateCustomerStatusRequest request)
    {
        var command = _mapper.Map<UpdateCustomerStatusCommand>(request);
        var updateStatus = await _mediator.Send(command);
        return updateStatus.Match(
            status => Ok(_mapper.Map<MessageResponse>(status)),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("find_customer")]
    [HasPermission(TableKey.Customers, PermissionType.Read)]
    public async Task<IActionResult> FindUser(FindUserRequest request)
    {
        var query = _mapper.Map<CustomerQuery>(request);
        var findUser = await _mediator.Send(query);

        return findUser.Match(user => Ok(_mapper.Map<FindCustomerResponse>(user)),
            errors => Problem(errors));
    }

    [HttpGet]
    // [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("orders")]
    [HasPermission(TableKey.Orders, PermissionType.Read)]
    public async Task<IActionResult> GetOrderManage()
    {
        var query = new OrderByBatchQuery();
        var orders = await _mediator.Send(query);

        return orders.Match(order => Ok(_mapper.Map<List<OrderManageResponse>>(order)),
            errors => Problem(errors));
    }

    [HttpPatch]
    // [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("order")]
    [HasPermission(TableKey.Orders, PermissionType.Update)]
    public async Task<IActionResult> UpdateOrderManage(UpdateOrderManageRequest request)
    {
        var command = _mapper.Map<UpdateOrderManageCommand>(request);
        var orderUpdate = await _mediator.Send(command);

        return orderUpdate.Match(order => Ok(_mapper.Map<MessageResponse>(order)),
            errors => Problem(errors));
    }

    [HttpGet]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("users")]
    [HasPermission(TableKey.Users, PermissionType.Read)]
    public async Task<IActionResult> CreateUser()
    {
        var query = new UserQuery();
        var createUser = await _mediator.Send(query);

        return createUser.Match(user => Ok(_mapper.Map<List<UserProfileWithStatusResponse>>(user)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [Route("user")]
    [HasPermission(TableKey.Users, PermissionType.Create)]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var createUser = await _mediator.Send(command);

        return createUser.Match(user => Ok(_mapper.Map<MessageResponse>(user)),
            errors => Problem(errors));
    }
}