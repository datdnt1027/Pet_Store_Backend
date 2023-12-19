using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<OrderProduct?> CheckOrderIsExist(Guid orderProductId, Guid customerId);
    Task<OrderProduct?> CheckProductOrderIsExist(Guid productId, Guid customerId);
    Task AddProductOrder(Guid customerId, Guid productId, int quantity);
    Task UpdateOrderProduct(OrderProduct orderProduct);
    Task UpdateProductPaymentInCart(Guid customerId, Guid orderId);
    Task AddOrder(pet_store_backend.domain.Entities.Orders.Order order);
    Task<List<OrderBriefResult>?> RetrieveOrderedProductsForCustomer(Guid customerId);
    Task DeleteOrderProduct(OrderProduct orderProduct);
    Task<OrderProduct?> RetrieveOrderProduct(Guid orderProductId);
    Task UpdateOrderStatusAccept(Guid orderId);
}