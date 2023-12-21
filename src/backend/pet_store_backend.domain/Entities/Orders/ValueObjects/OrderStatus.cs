namespace pet_store_backend.domain.Entities.Orders.ValueObjects;

public enum OrderStatus
{
    Accepted,
    Declined,
    Deliver,
    Receive
}

public enum OrderProductStatus
{
    Ordered,
    Completed,
    // Cancelled
}

public enum PaymentStatus
{
    Cancelled,
    COD,
    E_Wallet
};