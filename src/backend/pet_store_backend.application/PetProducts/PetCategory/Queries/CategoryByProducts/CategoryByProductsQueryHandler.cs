namespace pet_store_backend.application.PetProducts.PetCategory.Queries.CategoryByProducts;
using ErrorOr;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Common.Errors;

public class CategoryByProductsQueryHandler : IRequestHandler<CategoryByProductsQuery, ErrorOr<CategoryResult>>
{
    private readonly ICollectionRepository _categoryRepository;

    public CategoryByProductsQueryHandler(ICollectionRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<CategoryResult>> Handle(CategoryByProductsQuery query, CancellationToken cancellationToken)
    {
        var categoryByProducts = await _categoryRepository.GetCategoriesWithProductsInBatchAsync(query.CategoryId, query.Page);

        if (categoryByProducts == null)
        {
            return Errors.Category.NullCategory;
        }
        return categoryByProducts;
    }

}
