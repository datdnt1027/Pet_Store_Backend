using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class UserRole
    {
        public static Error UserRoleNotExist => Error.Conflict(
           code: "UserRole.NotExist",
           description: "User Role Is Not Exist");

    }
}