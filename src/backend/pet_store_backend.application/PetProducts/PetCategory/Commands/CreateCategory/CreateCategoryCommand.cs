using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;

namespace pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;

public record CreateCategoryCommand(
    string CategoryName,
    List<ProductCommand> Products
) : IRequest<ErrorOr<MessageResult>>;

public record ProductCommand(
    string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
   string ImageData
);


public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotNull().NotEmpty().MaximumLength(50); // Adjust the maximum length as needed

        RuleFor(x => x.Products)
            .NotEmpty().WithMessage("At least one product is required")
            .ForEach(product =>
            {
                product.ChildRules(productRule =>
                {
                    productRule.RuleFor(p => p.ProductName).NotNull().NotEmpty();
                    productRule.RuleFor(p => p.ProductDetail).NotNull().NotEmpty();
                    productRule.RuleFor(p => p.ProductQuantity).GreaterThan(0);
                    productRule.RuleFor(p => p.ProductPrice).GreaterThan(0);
                    productRule.RuleFor(p => p.ImageData).NotNull().NotEmpty();
                });
            });
    }
}
