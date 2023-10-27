using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.User.ValueObjects;

namespace pet_store_backend.domain.Entities.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        private User(UserId userId, string firstName, string lastName, string email, string passWord) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = passWord;
        }

        public static User Create(string firstName, string lastName, string email, string passWord)
        {
            return new(UserId.CreatUnique(), firstName, lastName, passWord, email);
        }

#pragma warning disable C8618
        protected User()
        {

        }
#pragma warning restore CS8618
    }
}
