using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error NoOrderProductDelete => Error.Conflict(
            code: "OrderProduct.ErrorDeleteOrderProduct",
            description: "Order Product not exist to delete !");
        public static Error OrderProductAddProblem => Error.Conflict(
            code: "OrderProduct.ErrorAddOrderProduct",
            description: "Order Product can not add !");
        public static Error NoOrderProductPayment => Error.Conflict(
            code: "OrderProduct.NoOrderInCart",
            description: "No Order Product have to pay !");

        public static Error NoQuantityOrderProductUpdate => Error.Conflict(
            code: "OrderProduct.NoOrderQuantityUpdate",
            description: "No Order Product Quantity Update !"
        );

        public static Error NoOrderInfoUpdate => Error.Conflict(
            code: "Order.NoOrderInfoUpdate",
            description: "No Order Info Update !"
        );

        public static Error NoOrderExist => Error.Conflict(
            code: "Order.NoOrderExist",
            description: "Order not exist !"
        );
    }
}