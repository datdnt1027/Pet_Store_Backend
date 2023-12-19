using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.infrastructure.Payment;

namespace pet_store_backend.infrastructure.Services
{
    public class PaymentProvider : IPaymentProvider
    {
        private readonly MomoSetting _momoSettings;
        private readonly IPasswordConfiguration _passwordConfiguration;
        public PaymentProvider(IOptions<MomoSetting> momoOptions, IPasswordConfiguration passwordConfiguration)
        {
            _momoSettings = momoOptions.Value;
            _passwordConfiguration = passwordConfiguration;
        }

        private record PaymentRequest
        (
            string StoreName,
            string PartnerCode,
            string PartnerName,
            string RequestType,
            string IpnUrl,
            string RedirectUrl,
            string OrderId,
            long Amount,
            string Lang,
            string OrderInfo,
            string RequestId,
            string ExtraData,
            string Signature
        );

        private record PaymentResult
        (
            string PartnerCode,
            string RequestId,
            string OrderId,
            long Amount,
            long ResponseTime,
            string OrderInfo,
            string OrderType,
            string TransId,
            int ResultCode,
            string PayType,
            string ExtraData,
            string Signature
        );

        public (bool, string?) GetLinkPaymentMomo(
            string RequestId,
            long Amount,
            string OrderId,
            string OrderInfo,
            string ExtraData = "",
            string Lang = "vi",
            string RequestType = "captureWallet"
        )
        {
            string rawHash = "accessKey=" + $"{_momoSettings.AccessKey}" +
                "&amount=" + $"{Amount}" +
                "&extraData=" + $"{ExtraData}" +
                "&ipnUrl=" + $"{_momoSettings.IpnUrl}" +
                "&orderId=" + $"{OrderId}" +
                "&orderInfo=" + $"{OrderInfo}" +
                "&partnerCode=" + $"{_momoSettings.PartnerCode}" +
                "&redirectUrl=" + $"{_momoSettings.ReturnUrl}" +
                "&requestId=" + $"{RequestId}" +
                "&requestType=" + $"{RequestType}";
            string signature = _passwordConfiguration.HmacSHA256(rawHash, _momoSettings.SecretKey);

            var paymentRequest = new PaymentRequest(
                StoreName: "Pet Store",
                PartnerCode: $"{_momoSettings.PartnerCode}",
                PartnerName: "Test",
                RequestType: RequestType,
                IpnUrl: $"{_momoSettings.IpnUrl}",
                RedirectUrl: $"{_momoSettings.ReturnUrl}",
                OrderId: OrderId,
                Amount: Amount,
                Lang: Lang,
                OrderInfo: OrderInfo,
                RequestId: RequestId,
                ExtraData: ExtraData,
                Signature: signature
            );

            using HttpClient client = new();
            var requestData = JsonConvert.SerializeObject(paymentRequest, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
            var requestContent = new StringContent(requestData, Encoding.UTF8,
                "application/json");

            var createPaymentLinks = client.PostAsync(_momoSettings.PaymentUrl, requestContent)
                .Result;

            if (createPaymentLinks.IsSuccessStatusCode)
            {
                var responseContent = createPaymentLinks.Content.ReadAsStringAsync().Result;
                return (true, responseContent);
            }
            else
            {
                return (false, createPaymentLinks.ReasonPhrase);
            }
        }

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
            string Signature)
        {
            string rawHash = "accessKey=" + $"{_momoSettings.AccessKey}" +
                "&amount=" + $"{Amount}" +
                "&extraData=" + $"{ExtraData}" +
                "&message=" + $"{Message}" +
                "&orderId=" + $"{OrderId}" +
                "&orderInfo=" + $"{OrderInfo}" +
                "&orderType=" + $"{OrderType}" +
                "&partnerCode=" + $"{_momoSettings.PartnerCode}" +
                "&payType=" + $"{PayType}" +
                "&requestId=" + $"{RequestId}" +
                "&responseTime=" + $"{ResponseTime}" +
                "&resultCode=" + $"{ResultCode}" +
                "&transId=" + $"{TransId}";
            string checkSignature = _passwordConfiguration.HmacSHA256(rawHash, _momoSettings.SecretKey);
            return Signature.Equals(checkSignature);
        }
    }
}