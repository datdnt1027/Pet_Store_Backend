namespace pet_store_backend.contracts.PetProducts;

public record CategoryResponse(
    string CategoryId,
    string CategoryName,
    List<ProductResponse> Products,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record ProductResponse(
    string ProductId,
    string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
    string ImageData,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record CategoryWithProductCountResponse(
    string CategoryId,
    string CategoryName,
    int ProductCount
);