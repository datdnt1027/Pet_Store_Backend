using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Entities.Orders;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<OrderProduct?> CheckOrderIsExist(Guid orderProductId, Guid customerId);
    Task<OrderProduct?> CheckProductOrderIsExist(Guid productId, Guid customerId);
    Task AddProductOrder(Guid customerId, Guid productId, int quantity);
    Task UpdateOrderProduct(OrderProduct orderProduct);
    Task UpdateProductPaymentE_WalletInCart(Guid customerId, Guid orderId);
    Task AddOrder(pet_store_backend.domain.Entities.Orders.Order order);
    Task<List<OrderProductBriefResult>?> RetrieveOrderedProductsForCustomer(Guid customerId);
    Task DeleteOrderProduct(OrderProduct orderProduct);
    Task<OrderProduct?> RetrieveOrderProduct(Guid orderProductId);
    Task UpdateOrderE_WalletStatusAccept(Guid orderId);
    Task<List<OrderResult>> RetrieveOrderHistory(Guid customerId, int page);
    Task UpdateOrderStatusCancelled(Guid orderId);
    Task UpdateProductPaymentCODInCart(Guid customerId, Guid orderId);
}