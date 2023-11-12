using ErrorOr;
using MediatR;
using pet_store_backend.application.PetProducts.Common;

namespace pet_store_backend.application.PetProducts.PetCategory.Queries.CategoryByProducts;

public record CategoryByProductsQuery(
    string CategoryId,
    int Page
) : IRequest<ErrorOr<CategoryResult>>;