namespace pet_store_backend.contracts.Order;

public record OrderProductRequest(
    string ProductId,
    int Quantity = 1
);