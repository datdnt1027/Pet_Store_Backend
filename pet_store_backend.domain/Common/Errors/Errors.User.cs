using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use");

        public static Error UserNotExist => Error.Conflict(
           code: "User.NotExist",
           description: "User Not Exist");


    }
}