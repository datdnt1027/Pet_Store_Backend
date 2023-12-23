using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;

namespace pet_store_backend.application.PetProducts.PetProduct.Queries;

public record ProductBySoldQuery(
    int NumOfProducts,
    int Page = 1
) : IRequest<ErrorOr<List<ProductResult>>>;

public class ProductBySoldQueryValidator : AbstractValidator<ProductBySoldQuery>
{
    public ProductBySoldQueryValidator()
    {
        RuleFor(x => x.NumOfProducts).GreaterThan(0); // Assuming Page should be a positive integer
        RuleFor(x => x.Page).GreaterThan(0);
    }
}

public class ProductBySoldQueryHandler : IRequestHandler<ProductBySoldQuery, ErrorOr<List<ProductResult>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICollectionRepository _collectionRepository;

    public ProductBySoldQueryHandler(ICollectionRepository collectionRepository, IOrderRepository orderRepository)
    {
        _collectionRepository = collectionRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<List<ProductResult>>> Handle(ProductBySoldQuery query, CancellationToken cancellationToken)
    {
        var productsByBatch = await _orderRepository.GetBestSellProduct(query.NumOfProducts, query.Page);
        productsByBatch.AddRange(await _collectionRepository.GetNumberProductsOrderByDate(query.NumOfProducts - productsByBatch.Count, query.Page));
        if (productsByBatch == null)
        {
            // Return an appropriate response for no products found
            return new List<ProductResult>(); // or any other default response
        }

        return productsByBatch;
    }
}
