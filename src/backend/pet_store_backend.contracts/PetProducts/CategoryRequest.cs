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

public record CategoryIdRequest(
    string CategoryId
);

public record ProductIdRequest(
    string ProductId
);
