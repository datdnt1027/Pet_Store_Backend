using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.domain.Entities.Orders;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderProduct> _orderProducts = new();
    public User User { get; private set; } = null!;
    public UserId? UserId { get; private set; } = null!;
    public DateTime OrderDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public DeliveryDate? ExpectedDelivery { get; private set; }
    public IReadOnlyList<OrderProduct> OrderProducts => _orderProducts.AsReadOnly();

    private Order(OrderId orderId,
        DateTime orderDate,
        OrderStatus orderStatus,
        UserId? userId,
        PaymentStatus paymentStatus) : base(orderId)
    {
        UserId = userId;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
        PaymentStatus = paymentStatus;
    }

    public static Order CreateOrder(Guid orderId
    )
    {
        return new(OrderId.Create(orderId), DateTime.Now, OrderStatus.Declined, null, ValueObjects.PaymentStatus.Cancelled);
    }

    public void UpdateUserId(UserId userId)
    {
        this.UserId = userId;
    }

    public void UpdateOrderAccept()
    {
        OrderStatus = OrderStatus.Accepted;
    }

    public void UpdatePaymentStatus(PaymentStatus paymentStatus)
    {
        PaymentStatus = paymentStatus;
    }

    public void UpdateOrderDelivery(DateTime expectedDeliveryDateStart, DateTime expectedDeliveryDateEnd)
    {
        ExpectedDelivery = DeliveryDate.Create(expectedDeliveryDateStart, expectedDeliveryDateEnd);
    }

    private Order()
    {

    }
}
