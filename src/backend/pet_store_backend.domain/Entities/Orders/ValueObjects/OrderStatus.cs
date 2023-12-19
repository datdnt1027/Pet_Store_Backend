namespace pet_store_backend.domain.Entities.Orders.ValueObjects;

public enum OrderStatus
{
    Accepted,
    Declined,
    Deliver,
    Recieve
}

public enum OrderProductStatus
{
    Ordered,
    Completed,
    // Cancelled
}