using Mapster;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;
using pet_store_backend.contracts.PetProducts;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;

namespace pet_store_backend.api.Common.Mapping;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>();

        config.NewConfig<CategoryResult, CategoryResponse>();

        config.NewConfig<CategoryWithProductCount, CategoryWithProductCountResponse>()
            .Map(dest => dest.CategoryId, src => src.Category.Id.Value)
            .Map(dest => dest.CategoryName, src => src.Category.CategoryName);

        config.NewConfig<Product, ProductResponse>()
            .Map(dest => dest.ProductId, src => src.Id.Value)
            .Map(dest => dest.ProductPrice, src => src.ProductPrice.Amount > 0 ? src.ProductPrice.Value : 0)
            .Map(dest => dest.ImageData, src => src.ImageData != null ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.ImageData)}" : null);
    }
}