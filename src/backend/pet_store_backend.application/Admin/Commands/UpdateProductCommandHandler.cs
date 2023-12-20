using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Admin.Commands;

public record UpdateProductCommand(
    string ProductId,
    string CategoryId,
    string ProductName,
    string ProductDetail,
    string? ProductQuantity,
    string? ProductPrice,
    byte[] ImageData,
    string Status
) : IRequest<ErrorOr<MessageResult>>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty().WithMessage("Product ID cannot be empty.")
            .Must(BeValidGuid).WithMessage("Invalid format for Product ID.");

        RuleFor(command => command.CategoryId)
            .Must(BeValidGuid).WithMessage("Invalid format for Category ID.")
            .When(command => command.CategoryId != null);

        RuleFor(x => x.ProductName)
            .Matches("^[a-zA-Z ]*$").WithMessage("Product name should only contain letters.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.")
            .When(command => command.ProductName != null);

        RuleFor(x => x.ProductDetail)
            .Matches("^[a-zA-Z ]*$").WithMessage("Product detail should only contain letters.")
            .MaximumLength(255).WithMessage("Product detail cannot exceed 255 characters.")
            .When(command => command.ProductDetail != null);

        RuleFor(x => x.ProductQuantity)
             .Must(x => int.TryParse(x, out _)).WithMessage("Product quantity must be a valid number.")
             .When(command => !string.IsNullOrEmpty(command.ProductQuantity));

        RuleFor(x => x.ProductPrice)
            .Must(x => double.TryParse(x, out _)).WithMessage("Product price must be a valid number.")
            .When(command => !string.IsNullOrEmpty(command.ProductPrice));

        RuleFor(x => x.Status)
            .Must(BeABoolean).WithMessage("Status must be a boolean value.")
            .When(command => command.Status != null);

        // Validate ImageData only if it is not null
        RuleFor(x => x.ImageData)
            .Must(BeValidImageData).WithMessage("Invalid image data.")
            .When(command => command.ImageData != null); ;
    }

    private bool BeValidImageData(byte[] imageData)
    {
        // Allow null or non-empty byte array
        return imageData == null || imageData.Length > 0;
    }

    private bool BeABoolean(string status)
    {
        return status == "0" || status == "1";
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<MessageResult>>
{
    private readonly ICollectionRepository _collectionRepository;
    public UpdateProductCommandHandler(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }
    public async Task<ErrorOr<MessageResult>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        bool flag = false;
        if (AllPropertiesNull(command))
        {
            return Errors.Product.NoInfoProductUpdate;
        }
        var validCategoryId = Guid.Parse(command.CategoryId);
        if (!await _collectionRepository.CheckCategoryIsValid(validCategoryId))
        {
            return Errors.Category.NullCategory;
        }

        var product = await _collectionRepository.GetProduct(Guid.Parse(command.ProductId));
        if (product is null)
        {
            return Errors.Product.NullProduct;
        }
        // Check and update properties only if they are different in AdminProfileAdminCommand
        if (command.CategoryId != null && validCategoryId != product.CategoryId?.Value)
        {
            if (!flag) flag = true;
            product.UpdateCategoryId(validCategoryId);
        }

        if (command.ProductName != null && command.ProductName != product.ProductName)
        {
            if (!flag) flag = true;
            product.UpdateProductName(command.ProductName);
        }

        if (command.ProductDetail != null && command.ProductDetail != product.ProductDetail)
        {
            if (!flag) flag = true;
            product.UpdateProductDetail(command.ProductDetail);
        }

        if (command.ProductQuantity != null && int.Parse(command.ProductQuantity) != product.ProductQuantity)
        {
            if (!flag) flag = true;
            product.UpdateProductQuantity(int.Parse(command.ProductQuantity));
        }

        if (command.ProductPrice != null && double.Parse(command.ProductPrice) != product.ProductPrice.Value)
        {
            if (!flag) flag = true;
            product.UpdateProductPrice(double.Parse(command.ProductPrice));
        }

        if (command.Status != null && ConvertToBool(command.Status) != product.Status)
        {
            if (!flag) flag = true;
            product.UpdateProductStatus(ConvertToBool(command.Status));
        }

        // if (command.Avatar != null && !ByteArraysEqual(command.Avatar, user.Avatar))
        if (command.ImageData != null)
        {
            if (!flag) flag = true;
            product.UpdateImageData(command.ImageData);
        }

        if (!flag)
            return Errors.Product.NoInfoProductUpdate;

        product.UpdateDateTimeProduct(DateTime.Now);
        // Save the updated admin profile to the repository
        await _collectionRepository.UpdateProduct(product);

        // You can return a success message or any other information if needed
        return new MessageResult("Product updated successfully");
    }

    private static bool ConvertToBool(string status)
    {
        return status == "1";
    }

    private static bool AllPropertiesNull(UpdateProductCommand command)
    {
        return command.CategoryId == null &&
            command.ProductName == null &&
            command.ProductDetail == null &&
            command.ProductQuantity != null &&
            command.ProductPrice != null &&
            command.ImageData == null &&
            command.Status == null;
    }
}