using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in user");

        public static Error UserNotExist => Error.Conflict(
           code: "User.NotExist",
           description: "User Not Exist");

        public static Error UserNotSignIn => Error.Unauthorized(
            code: "User.NotSignIn",
            description: "User Not Sign In"
        );

        public static Error NoUserInfoUpdate => Error.Conflict(
            code: "User.NoInfoUpdate",
            description: "No Info User Update");

    }
}