using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error IvalidCredentials => Error.Unauthorized(
            code: "Auth.InvalidCred",
            description: "Invalid Credential.");

        public static Error NotVerified => Error.Unauthorized(
            code: "Auth.NotVerfied",
            description: "User Not Verfied."
        );
        public static Error InvalidToken => Error.Unauthorized(
            code: "Auth.InvalidToken",
            description: "Invalid Token."
        );
        public static Error TokenExpire => Error.Unauthorized(
            code: "Auth.TokenExpire",
            description: "Token Expired. New Token is sending for you"
        );
        public static Error ForbidenPermission => Error.Unauthorized(
            code: "Auth.Permission",
            description: "User Don't Have Any Permission Or Permission Not Exist"
        );

    }
}