namespace pet_store_backend.contracts.Payment;

public record class MomoOneTimePaymentRequest(
// string RedirectUrl
);

public record MomoPaymentProductReturnRequest(
    string? RequestId,
    string? OrderId,
    long? Amount,
    long? ResponseTime,
    string? OrderInfo,
    string? OrderType,
    string? TransId,
    string? Message,
    int ResultCode,
    string? PayType,
    string? ExtraData,
    string? Signature
);