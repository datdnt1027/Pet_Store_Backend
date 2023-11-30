using ErrorOr;
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