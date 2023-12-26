using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;

namespace pet_store_backend.application.PetProducts.PetProduct.Queries;

public record SearchProductQuery(
    string SearchKey
) : IRequest<ErrorOr<List<ProductResult>>>;

public class SearchProductQueryValidator : AbstractValidator<SearchProductQuery>
{
    public SearchProductQueryValidator()
    {
        RuleFor(x => x.SearchKey)
            .NotEmpty()
            .Matches("^[a-zA-Z ]*$").WithMessage("Search should only contain letters.")
            .MaximumLength(50).WithMessage("Search cannot exceed 50 characters.");
    }
}

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, ErrorOr<List<ProductResult>>>
{
    private readonly ICollectionRepository _collectionRepository;

    public SearchProductQueryHandler(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<ErrorOr<List<ProductResult>>> Handle(SearchProductQuery query, CancellationToken cancellationToken)
    {
        var productsByBatch = await _collectionRepository.GetProductsSearch(query.SearchKey);

        if (productsByBatch == null)
        {
            // Return an appropriate response for no products found
            return new List<ProductResult>(); // or any other default response
        }

        return productsByBatch;
    }
}
