using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;

namespace pet_store_backend.infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _dbContext;
    public CategoryRepository(DataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task Add(Category category)
    {
        await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesWithProductsAsync()
    {
        return await _dbContext.Categories.Include(category => category.Products).ToListAsync();
    }

    public async Task<CategoryResult?> GetCategoriesWithProductsInBatchAsync(string categoryId, int page)
    {
        int pageSize = 5;

        // Calculate the number of products to skip based on the page number
        int productsToSkip = (page - 1) * pageSize;

        // Retrieve the category with the specified CategoryId and its products
        var category = await _dbContext.Categories
            .Include(c => c.Products)
            .Where(c => c.Id == CategoryId.Create(new Guid(categoryId)))
            .FirstOrDefaultAsync();

        // Check if the category exists
        if (category is null)
        {
            return null; // Handle the case where the category is not found
        }

        // Filter and select the products for the current page
        var productsInBatch = category.Products
            .Skip(productsToSkip)
            .Take(pageSize)
            .Select(product => new ProductResult(
                product.Id?.ToString() ?? "",
                product.ProductName,
                product.ProductDetail,
                product.ProductQuantity,
                product.ProductPrice.Value,
                product.ImageData ?? Array.Empty<byte>(),
                product.CreatedDateTime,
                product.UpdatedDateTime
            ))
            .ToList();

        // Create the CategoryResult
        var categoryResult = new CategoryResult(
            category.Id.Value.ToString(),
            category.CategoryName,
            productsInBatch,
            DateTime.Now, // You can adjust the created date as needed
            DateTime.Now // You can adjust the updated date as needed
        );

        return categoryResult;
    }

    public async Task<List<CategoryWithProductCount>> GetAllCategoriesWithNumberOfProducts()
    {
        var categoriesWithCounts = await _dbContext.Categories
            .Select(category => new CategoryWithProductCount(
            category,
            category.Products.Count
            ))
            .ToListAsync();

        return categoriesWithCounts;
    }
}