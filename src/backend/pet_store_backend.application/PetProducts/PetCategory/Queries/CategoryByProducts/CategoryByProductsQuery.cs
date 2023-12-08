using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.PetProducts.Common;

namespace pet_store_backend.application.PetProducts.PetCategory.Queries.CategoryByProducts;

public record CategoryByProductsQuery(
    string CategoryId,
    int Page
) : IRequest<ErrorOr<CategoryResult>>;

public class CategoryByProductsQueryValidator : AbstractValidator<CategoryByProductsQuery>
{
    public CategoryByProductsQueryValidator()
    {
        RuleFor(x => x.CategoryId).NotNull().NotEmpty();
        RuleFor(x => x.Page).GreaterThan(0); // Assuming Page should be a positive integer
    }
}
