using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;

namespace pet_store_backend.domain.Entities.PetProducts.PetProductCategory;

public sealed class Category : AggregateRoot<CategoryId>
{
    private readonly List<Product> _products = new();
    public string CategoryName { get; private set; }
    public IReadOnlyList<Product> Products => _products.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Category(
        CategoryId categoryId,
        string categoryName,
        List<Product> products,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(categoryId)
    {
        CategoryName = categoryName;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        _products = products;
    }

    public static Category Create(
        string categoryName,
        List<Product> products
    )
    {
        return new(CategoryId.CreatUnique(), categoryName, products ?? new(), DateTime.Now, DateTime.Now);
    }
#pragma warning disable CS8618
    private Category()
    {

    }
#pragma warning restore CS8618
}