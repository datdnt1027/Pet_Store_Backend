namespace pet_store_backend.application.Common.Interfaces.Services;

public interface IPaymentProvider
{
    (bool, string?) GetLinkPaymentMomo(
            string RequestId,
            long Amount,
            string OrderId,
            string OrderInfo,
            string RedirectUrl,
            string ExtraData = "",
            string Lang = "vi",
            string RequestType = "captureWallet"
        );
}