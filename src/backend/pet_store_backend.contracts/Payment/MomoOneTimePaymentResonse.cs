namespace pet_store_backend.contracts.Payment;

public record class MomoOneTimePaymentResponse(
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