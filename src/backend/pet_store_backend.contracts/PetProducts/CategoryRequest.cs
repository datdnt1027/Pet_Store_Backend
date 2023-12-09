namespace pet_store_backend.contracts.PetProducts;

public record CreateCategoryRequest(
    string CategoryName,
    List<ProductRequest> Products);

public record ProductRequest(
    string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
    string ImageData
);

public record CreateProductRequest(
    string CategoryId,
    string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
    byte[]? ImageData
);

public record UpdateProductRequest(
    string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
    byte[] ImageData,
    bool Status
);

public record CategoryIdRequest(
    string CategoryId
);

public record ProductIdRequest(
    string ProductId
);
