using pet_store_backend.contracts.PetProducts;

namespace pet_store_backend.contracts.Order;

public record OrderBriefResponse(
    Guid OrderProductId,
    ProductOrderBriefResponse Product,
    int Quantity
);

public record OrderProductResponse(
    int TotalQuantityProduct,
    long TotalPrice,
    List<OrderBriefResponse> Orders
);