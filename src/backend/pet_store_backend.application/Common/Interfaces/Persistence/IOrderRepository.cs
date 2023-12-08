using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<OrderProduct?> CheckOrderIsExist(Guid orderProductId);
    Task<bool> AddProductOrder(Guid productId, int quantity);
    Task UpdateOrderProduct(OrderProduct orderProduct);
    Task<List<OrderProductWithPrice>?> RetrieveTotalOrderProductPaymentInCart(OrderId orderId);
    Task AddOrder(pet_store_backend.domain.Entities.Orders.Order order);
    Task<List<OrderProduct>?> RetrieveOrderedProductsForUser();
}