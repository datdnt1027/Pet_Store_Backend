using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error OrderProductAddProblem => Error.Conflict(
            code: "Order.ErrorAddOrderProduct",
            description: "Order Product can not add !");

        public static Error NoOrderProductPayment => Error.Conflict(
            code: "Order.NoOrderInCart",
            description: "No Order have to pay !");
    }
}