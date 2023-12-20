using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.PetProducts.PetProduct.Commands;
using pet_store_backend.contracts;
using pet_store_backend.contracts.PetProducts;
using pet_store_backend.infrastructure.Authentication;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UserController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("product")]
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
}