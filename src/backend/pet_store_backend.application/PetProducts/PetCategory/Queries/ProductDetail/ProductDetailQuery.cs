using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.PetProducts.Common;

namespace pet_store_backend.application.PetProducts.PetCategory.Queries.ProductDetail;

public record ProductDetailQuery(
    string ProductId
) : IRequest<ErrorOr<ProductResult>>;

public class ProductDetailQueryValidator : AbstractValidator<ProductDetailQuery>
{
    public ProductDetailQueryValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}