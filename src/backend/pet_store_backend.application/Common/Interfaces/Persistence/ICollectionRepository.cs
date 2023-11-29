using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface ICollectionRepository
{
    Task Add(Category category);
    Task<List<Category>> GetAllCategoriesWithProductsAsync();
    Task<List<CategoryWithProductCount>> GetAllCategoriesWithNumberOfProducts();
    Task<CategoryResult?> GetCategoriesWithProductsInBatchAsync(string CategoryId, int page);
    Task<ProductResult?> GetProductDetail(string productId);
}