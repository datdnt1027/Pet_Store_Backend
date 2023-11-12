using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;

namespace pet_store_backend.application.PetProducts.Common
{
    public record CategoryWithProductCount(
        Category Category,
        int ProductCount
    );
    // public class CategoryWithProductCount
    // {
    //     public Category Category { get; init; } = null!;
    //     public int ProductCount { get; init; }
    // }
    public record CategoryResult(
        string CategoryId,
        string CategoryName,
        List<ProductResult> Products,
        DateTime CreatedDateTime,
        DateTime UpdatedDateTime
    );

    public record ProductResult(
        string ProductId,
        string ProductName,
        string ProductDetail,
        int ProductQuantity,
        double ProductPrice,
        byte[] ImageData,
        DateTime CreatedDateTime,
        DateTime UpdatedDateTime
    );

}