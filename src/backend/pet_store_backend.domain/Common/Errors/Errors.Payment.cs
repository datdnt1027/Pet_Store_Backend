using ErrorOr;

namespace pet_store_backend.domain.Common.Errors;

public static partial class Errors
{
    public static class Payment
    {
        public static Error PaymentProblem(string message) => Error.Failure(
            code: "Payment.Error",
            description: $"{message}");
    }
}