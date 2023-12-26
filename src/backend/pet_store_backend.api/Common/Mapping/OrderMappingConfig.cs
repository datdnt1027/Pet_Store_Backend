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
        config.NewConfig<MomoPaymentProductReturnRequest, MomoPaymentProductReturnCommand>();
        config.NewConfig<MomoPaymentReturnResult, MomoOneTimePaymentResponse>();
        config.NewConfig<OrderProductRequest, OrderProductCommand>();
        config.NewConfig<UpdateOrderProductQuantityRequest, UpdateOrderProductQuantityCommand>();
        // config.NewConfig<MomoOneTimePaymentRequest, MomoOneTimePaymentProductCommand>();
        config.NewConfig<MomoPaymentResponse, MomoOneTimePaymentResponse>();
        config.NewConfig<OrderProductResult, OrderProductResponse>();
        config.NewConfig<OrderProductBriefResult, OrderProductBriefResponse>();
        config.NewConfig<OrderProductResult, OrderProductResponse>()
            .Map(dest => dest.Orders, src => src.Orders);
        config.NewConfig<OrderResult, OrderResponse>();
        // config.NewConfig<ProductResultOrder, ProductResponseOrder>()
        //     .Map(dest => dest.ImageData, src => src.ImageData.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.ImageData)}" : null);
        config.NewConfig<CODPaymentResult, CODPaymentResponse>();
        config.NewConfig<OrderManageResult, OrderManageResponse>()
        .Map(dest => dest.OrderId, src => src.OrderId.ToString())
        .Map(dest => dest.ExpectedDeliveryEndDate, src => src.ExpectedDeliveryEndDate.ToString())
        .Map(dest => dest.ExpectedDeliveryStartDate, src => src.ExpectedDeliveryStartDate.ToString());
    }
}