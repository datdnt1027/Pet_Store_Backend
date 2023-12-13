using Mapster;
using pet_store_backend.application.Order.Commands;
using pet_store_backend.application.Order.Common;
using pet_store_backend.contracts.Order;
using pet_store_backend.contracts.Payment;

namespace pet_store_backend.api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderProductRequest, OrderProductCommand>();
        config.NewConfig<MomoOneTimePaymentRequest, MomoOneTimePaymentProductCommand>();
        config.NewConfig<PaymentResponse, MomoOneTimePaymentResponse>();
        config.NewConfig<OrderProductResult, OrderProductResponse>();
        config.NewConfig<OrderProductResult, OrderProductResponse>();
        config.NewConfig<ProductResultOrder, ProductResponseOrder>()
            .Map(dest => dest.ImageData, src => src.ImageData.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.ImageData)}" : null);
    }
}