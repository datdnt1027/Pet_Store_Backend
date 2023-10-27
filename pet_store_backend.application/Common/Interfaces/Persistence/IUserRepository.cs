using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);

    }
}
