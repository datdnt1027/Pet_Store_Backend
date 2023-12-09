using Mapster;
using pet_store_backend.application.Admin.Commands;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.application.PetProducts.PetCategory.Commands.CreateCategory;
using pet_store_backend.application.PetProducts.PetCategory.Queries.ProductDetail;
using pet_store_backend.application.PetProducts.PetProduct.Commands;
using pet_store_backend.contracts.PetProducts;

namespace pet_store_backend.api.Common.Mapping;

public class CollectionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>();
        config.NewConfig<CreateProductRequest, CreateProductCommand>();

        config.NewConfig<ProductIdRequest, ProductDetailQuery>();

        config.NewConfig<CategoryResult, CategoryResponse>();

        config.NewConfig<CategoryWithProductCount, CategoryWithProductCountResponse>()
            .Map(dest => dest.CategoryId, src => src.Category.Id.Value)
            .Map(dest => dest.CategoryName, src => src.Category.CategoryName);

        config.NewConfig<ProductBriefResult, ProductBriefResponse>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.ImageData, src => src.ImageData.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.ImageData)}" : null);

        config.NewConfig<ProductResult, ProductResponse>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.ImageData, src => src.ImageData.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.ImageData)}" : null);

        config.NewConfig<UpdateProductRequest, UpdateProductCommand>();
    }
}