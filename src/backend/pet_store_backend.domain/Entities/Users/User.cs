using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.domain.Entities.Users;

public sealed class User : Entity<UserId>
{

    private readonly List<Order>? _userProducts = new();
    public UserRole UserRole { get; private set; } = null!;
    public UserRoleId UserRoleId { get; private set; }
    public IReadOnlyList<Order>? UserProducts => _userProducts?.AsReadOnly();
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
        byte[] passwordSalt,
        UserRoleId userRoleId) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        UserRoleId = userRoleId;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        UserRoleId userRoleId)
    {
        var user = new User(
            UserId.CreatUnique(),
            firstName,
            lastName,
            email,
            passwordHash,
            passwordSalt,
            userRoleId);
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

    public void CreateVerificationToken(string verificationToken, DateTime tokenExpire)
    {
        VerificationToken = verificationToken;
        TokenExpires = tokenExpire;
    }

    public void CreatePasswordResetToken(string passwordResetToken, DateTime tokenExpire)
    {
        PasswordResetToken = passwordResetToken;
        TokenExpires = tokenExpire;
    }

    public void UpdateUserRoleId(UserRoleId userRoleId)
    {
        UserRoleId = userRoleId;
    }

#pragma warning disable CS8618
    private User()
    {

    }
#pragma warning restore CS8618
}

public sealed class Customer : Entity<CustomerId>
{

    public List<OrderProduct> _customerProducts = new();
    public UserRole CustomerRole { get; private set; } = null!;
    public UserRoleId CustomerRoleId { get; private set; }
    public IReadOnlyList<OrderProduct> CustomerProducts => _customerProducts.AsReadOnly();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; } = new byte[32];
    public byte[] PasswordSalt { get; private set; } = new byte[32];
    public string? VerificationToken { get; private set; }
    public DateTime? VerifiedAt { get; private set; }
    public string? PasswordResetToken { get; private set; }
    public DateTime? TokenExpires { get; private set; }


    private Customer(
        CustomerId customerId,
        string firstName,
        string lastName,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        UserRoleId customerRoleId) : base(customerId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        CustomerRoleId = customerRoleId;
    }

    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        UserRoleId customerRoleId)
    {
        var customer = new Customer(
            CustomerId.CreatUnique(),
            firstName,
            lastName,
            email,
            passwordHash,
            passwordSalt,
            customerRoleId);
        return customer;
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

    public void CreateVerificationToken(string verificationToken, DateTime tokenExpire)
    {
        VerificationToken = verificationToken;
        TokenExpires = tokenExpire;
    }

    public void CreatePasswordResetToken(string passwordResetToken, DateTime tokenExpire)
    {
        PasswordResetToken = passwordResetToken;
        TokenExpires = tokenExpire;
    }

    public void UpdateUserRoleId(UserRoleId customerRoleId)
    {
        CustomerRoleId = customerRoleId;
    }

#pragma warning disable CS8618
    private Customer()
    {

    }
#pragma warning restore CS8618
}
