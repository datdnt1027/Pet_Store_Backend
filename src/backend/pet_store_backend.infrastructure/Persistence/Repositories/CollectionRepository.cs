using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly DataContext _dbContext;
    public CollectionRepository(DataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task Add(Category category)
    {
        await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddProduct(Product product)
    {
        await _dbContext.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesWithProductsAsync()
    {
        return await _dbContext.Categories.Include(category => category.Products).ToListAsync();
    }

    public async Task<CategoryResult?> GetCategoriesWithProductsInBatchAsync(string categoryId, int page)
    {
        int pageSize = PageSize.CategorySize;

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
            .Where(p => p.Status == true)
            .Skip(productsToSkip)
            .Take(pageSize)
            .OrderBy(p => p.CategoryId)
            .Select(product => new ProductBriefResult(
                product.Id.Value,
                product.ProductName,
                product.ProductPrice.Value,
                product.ImageData ?? Array.Empty<byte>()
            ))
            .ToList();

        // Create the CategoryResult
        var categoryResult = new CategoryResult(
            category.Id.Value,
            category.CategoryName,
            productsInBatch,
            DateTime.Now, // You can adjust the created date as needed
            DateTime.Now // You can adjust the updated date as needed
        );

        return categoryResult;
    }

    public async Task<List<ProductResult>> GetProductsWithPage(int page)
    {
        int pageSize = 5;
        int productsToSkip = (page - 1) * pageSize;

        var productsInBatch = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.Status == true)
            .OrderBy(p => p.Id) // Add an OrderBy clause based on your sorting criteria
            .Skip(productsToSkip)
            .Take(pageSize)
            .Select(product => new ProductResult(
                product.CategoryId.Value,
                product.Id.Value,
                product.ProductName,
                product.ProductDetail,
                product.ProductQuantity,
                product.ProductPrice.Value,
                product.ImageData ?? Array.Empty<byte>(),
                product.CreatedDateTime,
                product.UpdatedDateTime
            )).ToListAsync();
        return productsInBatch;
    }

    public async Task<List<ProductResult>> GetNumberProductsOrderByDate(int numOfProducts, int page)
    {
        int productsToSkip = (page - 1) * numOfProducts;

        var productsInBatch = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.Status == true)
            .OrderByDescending(p => p.CreatedDateTime) // Add an OrderBy clause based on your sorting criteria
            .Skip(productsToSkip)
            .Take(numOfProducts)
            .Select(product => new ProductResult(
                product.CategoryId.Value,
                product.Id.Value,
                product.ProductName,
                product.ProductDetail,
                product.ProductQuantity,
                product.ProductPrice.Value,
                product.ImageData ?? Array.Empty<byte>(),
                product.CreatedDateTime,
                product.UpdatedDateTime
            )).ToListAsync();
        return productsInBatch;
    }

    public async Task<List<ProductResult>> GetProductsSearch(string searchKey)
    {
        var searchProduct = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.ProductName.Contains(searchKey))
            .Take(5)
            .Select(product => new ProductResult(
                product.CategoryId.Value,
                product.Id.Value,
                product.ProductName,
                product.ProductDetail,
                product.ProductQuantity,
                product.ProductPrice.Value,
                product.ImageData ?? Array.Empty<byte>(),
                product.CreatedDateTime,
                product.UpdatedDateTime
            )).ToListAsync();
        return searchProduct;
    }


    public async Task<ProductResult?> GetProductDetail(string productId, bool status = true)
    {
        var productDetail = await _dbContext.Products
            .AsNoTracking() // Make the query non-tracking
            .Where(p => p.Id == ProductId.Create(new Guid(productId)) && p.Status == status)
            .Select(product => new ProductResult(
                product.CategoryId.Value,
                product.Id.Value,
                product.ProductName,
                product.ProductDetail,
                product.ProductQuantity,
                product.ProductPrice.Value,
                product.ImageData ?? Array.Empty<byte>(),
                product.CreatedDateTime,
                product.UpdatedDateTime))
            .FirstOrDefaultAsync();

        return productDetail;
    }

    public async Task<List<CategoryWithProductCount>> GetAllCategoriesWithNumberOfProducts(bool status = true)
    {
        var categoriesWithCounts = await _dbContext.Categories
            .Select(category => new CategoryWithProductCount(
                category,
                category.Products.Count(p => p.Status == status)
            ))
            .ToListAsync();

        return categoriesWithCounts;
    }

    public async Task<bool> CheckCategoryIsValid(Guid categoryId)
    {
        var categoryExists = await _dbContext.Categories
            .AnyAsync(c => c.Id == CategoryId.Create(categoryId));

        return categoryExists;
    }

    public async Task<bool> CheckProductIsValid(Guid productId)
    {
        var productExists = await _dbContext.Products
            .AnyAsync(p => p.Id == ProductId.Create(productId) && p.Status == true);

        return productExists;
    }

    public async Task<Product?> GetProduct(Guid productId)
    {
        var productExists = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == ProductId.Create(productId));

        return productExists;
    }

    public async Task UpdateProduct(Product product)
    {
        _dbContext.Entry(product).State = EntityState.Modified;
        // Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

}