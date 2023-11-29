using ErrorOr;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;

public class CategoryQuery : IRequest<ErrorOr<List<CategoryWithProductCount>>>
{
    // No specific request data needed
}

public class CategoryWithNumberOfProductsHandler : IRequestHandler<CategoryQuery, ErrorOr<List<CategoryWithProductCount>>>
{
    private readonly ICollectionRepository _categoryRepository;

    public CategoryWithNumberOfProductsHandler(ICollectionRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<List<CategoryWithProductCount>>> Handle(CategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryWithNumberOfProdutcs = await _categoryRepository.GetAllCategoriesWithNumberOfProducts();
        return categoryWithNumberOfProdutcs;
    }
}
