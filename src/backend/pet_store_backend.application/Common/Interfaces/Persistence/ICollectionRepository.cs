using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface ICollectionRepository
{
    Task Add(Category category);
    Task AddProduct(Product product);
    Task<List<Category>> GetAllCategoriesWithProductsAsync();
    Task<List<ProductResult>> GetProductsSearch(string searchKey);
    Task<List<CategoryWithProductCount>> GetAllCategoriesWithNumberOfProducts(bool status = true);
    Task<CategoryResult?> GetCategoriesWithProductsInBatchAsync(string CategoryId, int page);
    Task<ProductResult?> GetProductDetail(string productId, bool status = true);
    Task<bool> CheckCategoryIsValid(Guid categoryId);
    Task<bool> CheckProductIsValid(Guid productId);
    Task<List<ProductResult>> GetProductsWithPage(int page);
    Task<Product?> GetProduct(Guid productId);
    Task UpdateProduct(Product product);
    Task<List<ProductResult>> GetNumberProductsOrderByDate(int numOfProducts, int page);
}