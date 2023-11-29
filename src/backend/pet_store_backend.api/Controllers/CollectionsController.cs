using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;
using pet_store_backend.application.PetProducts.PetCategory.Queries.CategoryByProducts;
using pet_store_backend.contracts;
using pet_store_backend.contracts.PetProducts;
namespace pet_store_backend.api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class CollectionsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CollectionsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCatory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var createCategoryRequest = await _mediator.Send(command);

        return createCategoryRequest.Match(category => Ok(_mapper.Map<MessageResponse>(category)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesWithCountProducts()
    {
        var query = new CategoryQuery(); // Create a new CategoryQuery
        var categoriesWithProducts = await _mediator.Send(query); // Send the query to the handler

        return categoriesWithProducts.Match(
            categories => Ok(_mapper.Map<IEnumerable<CategoryWithProductCountResponse>>(categories)), // Map and return successful result
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("category")]
    public async Task<IActionResult> GetProductsByCategories([FromBody] CategoryIdRequest categoryQuery, [FromQuery] int page = 1)
    {
        var query = new CategoryByProductsQuery(categoryQuery.CategoryId, page); // Create a new CategoryQuery
        var categoriesWithProducts = await _mediator.Send(query); // Send the query to the handler

        return categoriesWithProducts.Match(
            categories => Ok(_mapper.Map<CategoryResponse>(categories)), // Map and return successful result
            errors => Problem(errors)
        );
    }

}