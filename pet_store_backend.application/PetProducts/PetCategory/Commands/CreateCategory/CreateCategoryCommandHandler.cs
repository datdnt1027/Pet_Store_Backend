
using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using CategoryDomain = pet_store_backend.domain.Entities.PetProducts.PetProductCategory.Category;

namespace pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<MessageResult>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<MessageResult>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        /* 
        TODO: 
        1. Create Category
        2. Persist Category
         */
        var category = CategoryDomain.Create(
            request.CategoryName,
            request.Products.ConvertAll(product => Product.Create(
                product.ProductName,
                product.ProductDetail,
                product.ProductQuantity,
                product.ProductPrice,
                File.ReadAllBytes(product.ImageData)
            ))
        );

        await _categoryRepository.Add(category);

        return new MessageResult(Message: "Created Successfully !");
    }
}