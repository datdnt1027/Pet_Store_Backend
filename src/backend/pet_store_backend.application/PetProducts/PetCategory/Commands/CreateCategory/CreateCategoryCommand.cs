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
    string ProductQuantity,
    string ProductPrice,
    byte[] ImageData
);


public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotNull().NotEmpty().MaximumLength(50); // Adjust the maximum length as needed

        RuleFor(x => x.Products)
            .ForEach(product =>
            {
                product.ChildRules(productRule =>
                {
                    productRule.RuleFor(x => x.ProductName)
                        .NotEmpty()
                        .WithMessage("Product name is required.")
                        .Matches("^[a-zA-Z ]*$").WithMessage("Product name should only contain letters.")
                        .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

                    productRule.RuleFor(x => x.ProductDetail)
                        .NotEmpty()
                        .WithMessage("Product detail is required.")
                        .Matches("^[a-zA-Z ]*$").WithMessage("Product detail should only contain letters.")
                        .MaximumLength(255).WithMessage("Product detail cannot exceed 255 characters.");

                    productRule.RuleFor(x => x.ProductQuantity)
                        .Must(x => int.TryParse(x, out int quantity) && quantity > 0)
                        .WithMessage("Product quantity must be a valid number.");

                    productRule.RuleFor(x => x.ProductPrice)
                        .NotEmpty().WithMessage("Product Price can not be null.")
                        .Must(BeValidVndAmount)
                        .WithMessage("Product price must be a valid number.");

                    // ImageData can be null, but if provided, it should be of a specific type
                    productRule.RuleFor(x => x.ImageData)
                        // .NotEmpty().When(x => x.ImageData == null)
                        // .WithMessage("Image data is required.")
                        .Must(x => x is byte[]).When(x => x.ImageData != null)
                        .WithMessage("Image data must be in byte array format.");
                });
            })
            .When(commad => commad.Products != null);
    }

    private bool BeValidVndAmount(string? value)
    {
        if (value != null)
        {
            if (double.TryParse(value, out double result) && result > 0)
            {
                // Check if the value is divisible by 1000
                return result % 1000 == 0;
            }
        }
        return false;
    }
}
