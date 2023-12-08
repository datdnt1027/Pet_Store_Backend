using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.PetProducts.PetProduct.Queries;

public record ProductByBatchQuery(
    int Page
) : IRequest<ErrorOr<List<ProductResult>>>;

public class ProductByBatchQueryValidator : AbstractValidator<ProductByBatchQuery>
{
    public ProductByBatchQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0); // Assuming Page should be a positive integer
    }
}

public class CategoryByProductsQueryHandler : IRequestHandler<ProductByBatchQuery, ErrorOr<List<ProductResult>>>
{
    private readonly ICollectionRepository _collectionRepository;

    public CategoryByProductsQueryHandler(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<ErrorOr<List<ProductResult>>> Handle(ProductByBatchQuery query, CancellationToken cancellationToken)
    {
        var productsByBatch = await _collectionRepository.GetProductsWithPage(query.Page);

        if (productsByBatch == null)
        {
            // Return an appropriate response for no products found
            return new List<ProductResult>(); // or any other default response
        }

        return productsByBatch;
    }
}
