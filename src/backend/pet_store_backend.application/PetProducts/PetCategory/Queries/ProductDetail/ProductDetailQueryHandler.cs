using ErrorOr;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.PetProducts.PetCategory.Queries.ProductDetail
{
    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, ErrorOr<ProductResult>>
    {
        private readonly ICollectionRepository _collectionRepository;
        public ProductDetailQueryHandler(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
        public async Task<ErrorOr<ProductResult>> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ProductId, out var productIdGuid))
            {
                return Errors.Product.InvalidProductId;
            }

            var productDetail = await _collectionRepository.GetProductDetail(productIdGuid.ToString());

            if (productDetail is null)
            {
                return Errors.Product.NullProduct;
            }

            return productDetail;
        }

    }
}