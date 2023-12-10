namespace pet_store_backend.infrastructure.Payment;

public class MomoSetting
{
    public const string SectionName = "MomoSettings";
    public string PartnerCode { get; init; } = string.Empty;
    public string ReturnUrl { get; init; } = string.Empty;
    public string IpnUrl { get; init; } = string.Empty;
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public string PaymentUrl { get; init; } = string.Empty;
}