public record ProductResponseOrder(
    string ProductName,
    string ProductDetail,
    double ProductPrice,
    byte[] ImageData,
    int Quantity
);

public record OrderProductResponse(
    int TotalQuantityProduct,
    long TotalPrice,
    List<ProductResponseOrder> Products
);