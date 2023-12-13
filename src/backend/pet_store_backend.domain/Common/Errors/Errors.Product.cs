using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NullProduct => Error.Conflict(
            code: "Product.IsNull",
            description: "Product is not exist !");

        public static Error InvalidProductId => Error.Conflict(
            code: "ProductId.Invalid",
            description: "ProductId is Invalid"
        );
    }
}