namespace pet_store_backend.contracts.PetProducts;

public record CreateCategoryRequest(
    string CategoryName,
    List<ProductRequest>? Products);

public record ProductRequest(
    string ProductName,
    string ProductDetail,
    string ProductQuantity,
    string ProductPrice,
    byte[]? ImageData
);

public record CreateProductRequest(
    string CategoryId,
    string ProductName,
    string ProductDetail,
    string ProductQuantity,
    string ProductPrice,
    byte[]? ImageData
);

public record UpdateProductRequest(
    string ProductId,
    string? CategoryId,
    string? ProductName,
    string? ProductDetail,
    string? ProductQuantity,
    string? ProductPrice,
    byte[]? ImageData,
    string? Status
);

public record CategoryIdRequest(
    string CategoryId
);

public record ProductIdRequest(
    string ProductId
);


