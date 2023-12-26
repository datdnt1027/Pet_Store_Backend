using pet_store_backend.contracts.PetProducts;

namespace pet_store_backend.contracts.Order;

public record OrderProductBriefResponse(
    Guid OrderProductId,
    ProductOrderBriefResponse Product,
    int Quantity
);

public record OrderProductResponse(
    int TotalQuantityProduct,
    long TotalPrice,
    List<OrderProductBriefResponse> Orders
);

public record OrderResponse(
    DateTime OrderDate,
    string PaymentType,
    IEnumerable<OrderProductBriefResponse> Orders
);

public record OrderManageResponse(
    string OrderId,
    string OrderStatus,
    string PaymentStatus,
    string ExpectedDeliveryStartDate,
    string ExpectedDeliveryEndDate,
    string CustomerEmail
);