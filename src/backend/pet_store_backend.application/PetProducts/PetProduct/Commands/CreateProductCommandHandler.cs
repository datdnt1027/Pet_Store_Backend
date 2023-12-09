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
    int ProductQuantity,
    double ProductPrice,
    byte[] ImageData
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

        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.ProductDetail).NotEmpty().WithMessage("Product detail is required.");
        RuleFor(x => x.ProductQuantity).GreaterThan(0).WithMessage("Product quantity must be greater than 0.");
        RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Product price must be greater than 0.");

        // ImageData can be null, but if provided, it should be of a specific type
        RuleFor(x => x.ImageData)
            // .NotEmpty().When(x => x.ImageData == null)
            // .WithMessage("Image data is required.")
            .Must(x => x is byte[]).When(x => x.ImageData != null)
            .WithMessage("Image data must be in byte array format.");
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
            request.ProductQuantity,
            request.ProductPrice,
            CategoryId.Create(categoryId),
            request.ImageData
        );

        await _collectionRepository.AddProduct(product);

        return new MessageResult("Create Product Success!");
    }
}
