using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.PetProducts.PetCategory.Queries.CategoryByProducts;
using pet_store_backend.application.PetProducts.PetCategory.Queries.ProductDetail;
using pet_store_backend.application.PetProducts.PetProduct.Queries;
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

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesWithCountProducts()
    {
        var query = new CategoryQuery();
        var categoriesWithProducts = await _mediator.Send(query);

        return categoriesWithProducts.Match(
            categories => Ok(_mapper.Map<IEnumerable<CategoryWithProductCountResponse>>(categories)),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Route("category")]
    public async Task<IActionResult> GetProductsByCategories([FromBody] CategoryIdRequest categoryQuery, [FromQuery] int page = 1)
    {
        var query = new CategoryByProductsQuery(categoryQuery.CategoryId, page);
        var categoriesWithProducts = await _mediator.Send(query);

        return categoriesWithProducts.Match(
            categories => Ok(_mapper.Map<CategoryResponse>(categories)),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetProductsByPage([FromQuery] int page = 1)
    {
        var query = new ProductByBatchQuery(page);
        var productsByBatch = await _mediator.Send(query);

        return productsByBatch.Match(
            products => Ok(_mapper.Map<List<ProductResponse>>(products)),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("products_search")]
    public async Task<IActionResult> GetProductsSearch([FromQuery] string search)
    {
        var query = new SearchProductQuery(search);
        var productsSearch = await _mediator.Send(query);

        return productsSearch.Match(
            products => Ok(_mapper.Map<List<ProductResponse>>(products)),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Route("product")]
    public async Task<IActionResult> GetProductDetail(ProductIdRequest productQuery)
    {
        var query = new ProductDetailQuery(productQuery.ProductId);
        var productDetail = await _mediator.Send(query);

        return productDetail.Match(
           product => Ok(_mapper.Map<ProductResponse>(product)),
           errors => Problem(errors)
       );
    }

}