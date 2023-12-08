using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.domain.Entities.Orders;

public sealed class OrderProduct : Entity<OrderProductId>
{
    public Customer Customer { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; } = null!;
    public OrderId OrderId { get; private set; } = null!;
    public Order Order { get; private set; } = null!;
    public Product Product { get; private set; } = null!;
    public ProductId ProductId { get; private set; } = null!;
    public int Quantity { get; private set; }
    public OrderProductStatus OrderProductStatus { get; private set; }

    private OrderProduct(OrderProductId orderProductId,
                          CustomerId customerId,
                         ProductId productId,
                         int quantity,
                         OrderProductStatus orderProductStatus) : base(orderProductId)
    {
        CustomerId = customerId;
        ProductId = productId;
        Quantity = quantity;
        OrderProductStatus = orderProductStatus;
    }

    private OrderProduct(
        Product Product,
        int Quantity
    )
    {
        this.Product = Product;
        this.Quantity = Quantity;
    }

    public static OrderProduct Create(
        CustomerId customerId,
        ProductId productId,
        int quantity
    )
    {
        return new OrderProduct(
            OrderProductId.CreateUnique(),
            customerId,
            productId,
            quantity,
            OrderProductStatus.Ordered
        );
    }

    public static OrderProduct Retrive(OrderProductId orderProductId,
       CustomerId customerId,
       ProductId productId,
       int quantity,
       OrderProductStatus orderProductStatus
   )
    {
        return new OrderProduct(
            orderProductId,
            customerId,
            productId,
            quantity,
            orderProductStatus
        );
    }

    public void CompletedOrder(OrderId orderId)
    {
        OrderProductStatus = OrderProductStatus.Completed;
        OrderId = orderId;
    }

    public void CancelOrder()
    {
        OrderProductStatus = OrderProductStatus.Cancelled;
    }

    public void UpdateQuantityOrderProduct(int newQuantity)
    {
        Quantity = newQuantity;
    }

    public void CreateOrderIdForOrderProduct(OrderId orderId)
    {
        OrderId = orderId;
    }

    private OrderProduct()
    {

    }

}