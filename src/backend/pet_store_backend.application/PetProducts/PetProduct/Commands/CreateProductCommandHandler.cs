using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;

namespace pet_store_backend.application.PetProducts.PetProduct.Commands;

public record CreateProductCommand(
    string CategoryId,
    string ProductName,
    string ProductDetail,
    string ProductQuantity,
    string ProductPrice,
    byte[]? ImageData
) : IRequest<ErrorOr<MessageResult>>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private static bool BeAValidGuid(string categoryId)
    {
        return Guid.TryParse(categoryId, out _);
    }
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.CategoryId)
           .NotEmpty().WithMessage("Category ID is required.")
           .Must(BeAValidGuid).WithMessage("Category ID must be a valid GUID.");

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .Matches("^[a-zA-Z ]*$").WithMessage("Product name should only contain letters.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(x => x.ProductDetail)
            .NotEmpty()
            .WithMessage("Product detail is required.")
            .Matches("^[a-zA-Z ]*$").WithMessage("Product detail should only contain letters.")
            .MaximumLength(255).WithMessage("Product detail cannot exceed 255 characters.");

        RuleFor(x => x.ProductQuantity)
            .Must(x => int.TryParse(x, out int quantity) && quantity > 0)
            .WithMessage("Product quantity must be a valid number.");

        RuleFor(x => x.ProductPrice)
            .NotEmpty().WithMessage("Product Price can not be null.")
            .Must(BeValidVndAmount)
            .WithMessage("Product price must be a valid number.");

        // ImageData can be null, but if provided, it should be of a specific type
        RuleFor(x => x.ImageData)
            // .NotEmpty().When(x => x.ImageData == null)
            // .WithMessage("Image data is required.")
            .Must(x => x is byte[]).When(x => x.ImageData != null)
            .WithMessage("Image data must be in byte array format.");
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

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<MessageResult>>
{
    private readonly ICollectionRepository _collectionRepository;
    public CreateProductCommandHandler(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }
    public async Task<ErrorOr<MessageResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categoryId = Guid.Parse(request.CategoryId);
        if (!await _collectionRepository.CheckCategoryIsValid(categoryId))
        {
            return Errors.Category.NullCategory;
        }

        var product = Product.Create(
            request.ProductName,
            request.ProductDetail,
            int.Parse(request.ProductQuantity),
            double.Parse(request.ProductPrice),
            CategoryId.Create(categoryId),
            request.ImageData
        );

        await _collectionRepository.AddProduct(product);

        return new MessageResult("Create Product Success!");
    }
}
