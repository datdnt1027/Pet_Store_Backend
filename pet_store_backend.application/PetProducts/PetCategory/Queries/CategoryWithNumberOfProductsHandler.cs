using ErrorOr;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;

public class CategoryQuery : IRequest<ErrorOr<List<CategoryWithProductCount>>>
{
    // No specific request data needed
}

public class CategoryQueryHandler : IRequestHandler<CategoryQuery, ErrorOr<List<CategoryWithProductCount>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<List<CategoryWithProductCount>>> Handle(CategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryWithNumberOfProdutcs = await _categoryRepository.GetAllCategoriesWithNumberOfProducts();
        return categoryWithNumberOfProdutcs;
    }
}
