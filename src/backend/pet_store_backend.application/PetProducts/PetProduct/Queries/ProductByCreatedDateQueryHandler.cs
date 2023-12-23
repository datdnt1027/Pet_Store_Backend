using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;

namespace pet_store_backend.application.PetProducts.PetProduct.Queries;

public record ProductByDateCreatedQuery(
    int NumOfProducts,
    int Page = 1
) : IRequest<ErrorOr<List<ProductResult>>>;

public class ProductByDateCreatedQueryValidator : AbstractValidator<ProductByDateCreatedQuery>
{
    public ProductByDateCreatedQueryValidator()
    {
        RuleFor(x => x.NumOfProducts).GreaterThan(0); // Assuming Page should be a positive integer
        RuleFor(x => x.Page).GreaterThan(0);
    }
}

public class ProductByDateCreatedQueryHandler : IRequestHandler<ProductByDateCreatedQuery, ErrorOr<List<ProductResult>>>
{
    private readonly ICollectionRepository _collectionRepository;

    public ProductByDateCreatedQueryHandler(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<ErrorOr<List<ProductResult>>> Handle(ProductByDateCreatedQuery query, CancellationToken cancellationToken)
    {
        var productsByBatch = await _collectionRepository.GetNumberProductsOrderByDate(query.NumOfProducts, query.Page);

        if (productsByBatch == null)
        {
            // Return an appropriate response for no products found
            return new List<ProductResult>(); // or any other default response
        }

        return productsByBatch;
    }
}
