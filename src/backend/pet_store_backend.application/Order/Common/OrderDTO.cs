using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.Orders;

namespace pet_store_backend.application.Order.Common;

public record OrderProductWithPrice(
    OrderProduct OrderProduct,
    double Price
);

public record PaymentResponse
(
    string PartnerCode,
    string RequestId,
    string OrderId,
    long Amount,
    long ResponseTime,
    string Message,
    string ResultCode,
    string PayUrl,
    string DeepLink,
    string QrCodeUrl
);

// public record ProductResultOrder(
//     string ProductName,
//     string ProductDetail,
//     double ProductPrice,
//     byte[] ImageData,
//     int Quantity
// );
public record OrderBriefResult(
    Guid OrderProductId,
    ProductOrderBriefResult Product,
    int Quantity
);
public record OrderProductResult(
    int TotalQuantityProduct,
    long TotalPrice,
    List<OrderBriefResult> Orders
);