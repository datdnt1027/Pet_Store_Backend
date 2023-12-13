using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Authentication.Common
{
    public record AuthenticationUserResult(
       pet_store_backend.domain.Entities.Users.User User,
        string Token);

    public record AuthenticationCustomerResult(
       Customer Customer,
        string Token);
}
