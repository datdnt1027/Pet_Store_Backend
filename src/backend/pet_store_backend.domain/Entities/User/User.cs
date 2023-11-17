using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.User.ValueObjects;

namespace pet_store_backend.domain.Entities.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; } = new byte[32];
        public byte[] PasswordSalt { get; private set; } = new byte[32];
        public string? VerificationToken { get; private set; }
        public DateTime? VerifiedAt { get; private set; }
        public string? PasswordResetToken { get; private set; }
        public DateTime? TokenExpires { get; private set; }

        private User(
            UserId userId,
            string firstName,
            string lastName,
            string email,
            byte[] passwordHash,
            byte[] passwordSalt) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public static User Create(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            var user = new User(UserId.CreatUnique(), firstName, lastName, email, passwordHash, passwordSalt);
            user.TokenExpires = DateTime.Now.AddMinutes(5);
            return user;
        }

        public void UpdateVerifiedAt(DateTime verifiedAt)
        {
            VerifiedAt = verifiedAt;
            VerificationToken = null;
            PasswordResetToken = null;
            TokenExpires = null;
        }

        public void UpdatePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void CreateVerificationToken(string verificationToken)
        {
            VerificationToken = verificationToken;
            TokenExpires = DateTime.Now.AddMinutes(5);
        }

        public void CreatePasswordResetToken(string passwordResetToken)
        {
            PasswordResetToken = passwordResetToken;
            TokenExpires = DateTime.Now.AddMinutes(5);
        }

#pragma warning disable CS8618
        private User()
        {

        }
#pragma warning restore CS8618
    }
}
