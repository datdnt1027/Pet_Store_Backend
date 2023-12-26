using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;

namespace pet_store_backend.application.Order.Common;

public record OrderProductWithPrice(
    OrderProduct OrderProduct,
    double Price
);

public record MomoPaymentResponse
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

public record MomoPaymentReturnResult(
    string OrderId,
    string PaymentMessage,
    string PaymentDate,
    decimal Amount,
    string Signature
);

// public record ProductResultOrder(
//     string ProductName,
//     string ProductDetail,
//     double ProductPrice,
//     byte[] ImageData,
//     int Quantity
// );
public record OrderProductBriefResult(
    Guid OrderProductId,
    ProductOrderBriefResult Product,
    int Quantity
);

public record OrderProductResult(
    int TotalQuantityProduct,
    long TotalPrice,
    List<OrderProductBriefResult> Orders
);

public record OrderResult(
    DateTime? OrderDate,
    PaymentStatus? PaymentType,
    IEnumerable<OrderProductBriefResult> Orders
);

public record OrderManageResult(
    Guid OrderId,
    string OrderStatus,
    string PaymentStatus,
    string ExpectedDeliveryStartDate,
    string ExpectedDeliveryEndDate,
    string CustomerEmail
);

public record CODPaymentResult(
    string OrderId,
    string PaymentMessage,
    string PaymentDate,
    decimal Amount
);
