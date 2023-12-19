namespace pet_store_backend.application.Common.Interfaces.Services;

public interface IPaymentProvider
{
        (bool, string?) GetLinkPaymentMomo(
                string RequestId,
                long Amount,
                string OrderId,
                string OrderInfo,
                string ExtraData = "",
                string Lang = "vi",
                string RequestType = "captureWallet"
        );

        public bool IsValidSignature(
                string RequestId,
                string OrderId,
                long Amount,
                long ResponseTime,
                string OrderInfo,
                string OrderType,
                string TransId,
                string Message,
                int ResultCode,
                string PayType,
                string ExtraData,
                string Signature
        );
}