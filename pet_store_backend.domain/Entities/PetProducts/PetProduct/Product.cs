using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;

namespace pet_store_backend.domain.Entities.PetProducts.PetProduct;

public sealed class Product : Entity<ProductId>
{
    public string ProductName { get; private set; }
    public string ProductDetail { get; private set; }
    public int ProductQuantity { get; private set; }
    public Price ProductPrice { get; private set; }
    public byte[]? ImageData { get; private set; }
    public bool Status { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Product(
        ProductId productId,
        string productName,
        string productDetail,
        int productQuantity,
        Price productPrice,
        byte[]? imageData,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        bool status)
        : base(productId)
    {
        ProductName = productName;
        ProductDetail = productDetail;
        ProductQuantity = productQuantity;
        ProductPrice = productPrice;
        ImageData = imageData;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        Status = status;
    }

    public static Product Create(string productName,
        string productDetail,
        int productQuantity,
        double productPrice,
        byte[]? imageData = null)
    {
        return new Product(ProductId.CreatUnique(),
            productName,
            productDetail,
            productQuantity,
            Price.CreateNew(productPrice),
            imageData,
            DateTime.Now,
            DateTime.Now,
            true);
    }

#pragma warning disable CS8618
    private Product()
    {

    }
#pragma warning restore CS8618
}