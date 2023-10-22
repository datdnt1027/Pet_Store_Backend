using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error IvalidCredentials => Error.Unauthorized(
            code: "Auth.InvalidCred",
            description: "Invalid Credentials.");

    }
}