using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NullCategory => Error.Conflict(
            code: "Category.IsNull",
            description: "CategoryId is not exist !");
    }
}