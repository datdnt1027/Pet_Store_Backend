using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.infrastructure.Persistence.Common;
using pet_store_backend.application.Customer.Common;

namespace pet_store_backend.infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dbContext;

    public UserRepository(DataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    // Add Customer
    public async Task Add(Customer customer)
    {
        await _dbContext.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddUser(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    //Update information for Customer
    public async Task Update(Customer customer)
    {
        _dbContext.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    // This is User Email
    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users
            .Where(u => u.Email == email)
            .Include(u => u.UserRole) // Include the UserRole navigation property
            .FirstOrDefaultAsync();

        return user;
    }

    // This is Customer Email
    public async Task<Customer?> GetCustomerByEmail(string email)
    {
        var customer = await _dbContext.Customers
            .Where(u => u.Email == email)
            .Include(u => u.CustomerRole) // Include the UserRole navigation property
            .FirstOrDefaultAsync();

        return customer;
    }

    public async Task<bool> CheckUserExistByEmail(string email)
    {
        var checkUserEmail = await _dbContext.Users
            .AnyAsync(u => u.Email == email);

        return checkUserEmail;
    }



    public async Task<UserProfileWithStatusResult?> GetCustomerByEmailForAdmin(string email)
    {
        var customer = await _dbContext.Customers
            .Where(u => u.Email == email)
            .Select(u => new UserProfileWithStatusResult(
                u.Id.Value,
                u.FirstName,
                u.LastName,
                u.Gender,
                u.Email,
                u.Address ?? "",
                u.Avatar ?? Array.Empty<byte>(),
                u.PhoneNumber ?? "",
                u.Status
            ))
            .FirstOrDefaultAsync();

        return customer;
    }

    public async Task<UserProfileWithStatusResult?> GetCustomerByPhoneNumberForAdmin(string phoneNumber)
    {
        var customer = await _dbContext.Customers
            .Where(u => u.PhoneNumber == phoneNumber)
            .Select(u => new UserProfileWithStatusResult(
                u.Id.Value,
                u.FirstName,
                u.LastName,
                u.Gender,
                u.Email,
                u.Address ?? "",
                u.Avatar ?? Array.Empty<byte>(),
                u.PhoneNumber ?? "",
                u.Status
            ))
            .FirstOrDefaultAsync();

        return customer;
    }

    public async Task<Customer?> GetCustomerByVerificationToken(string verificationToken)
    {
        var customer = await _dbContext.Customers.SingleOrDefaultAsync(u => u.VerificationToken == verificationToken);
        return customer;
    }

    public async Task<Customer?> GetCustomerByResetPasswordToken(string resetPasswordToken)
    {
        var customer = await _dbContext.Customers
            .SingleOrDefaultAsync(u => u.PasswordResetToken == resetPasswordToken);
        return customer;
    }

    public async Task<List<UserPermission>> GetUserPermissionsAsync(UserRoleId userRoleId)
    {
        var userPermissionIds = await _dbContext.UserPermissions
            .Where(up => up.UserRoleId == userRoleId)
            .ToListAsync();

        return userPermissionIds;
    }

    public async Task<UserRoleId?> GetGuestRoleId()
    {
        var userRole = await _dbContext.UserRoles
            .Where(ur => ur.UserRoleName.Equals(UserRoleKey.UserRoleName))
            .FirstOrDefaultAsync();
        return userRole?.Id;
    }

    public async Task<UserRoleId?> GetUserRoleId(string userRoleName)
    {
        var userRole = await _dbContext.UserRoles
            .Where(ur => ur.UserRoleName.Equals(userRoleName))
            .FirstOrDefaultAsync();
        return userRole?.Id;
    }

    public async Task<UserProfileResult?> RetrieveUserProfile(Guid userId)
    {
        var adminProfile = await _dbContext.Customers
            .Where(u => u.Id == CustomerId.Create(userId))
            .FirstOrDefaultAsync();

        if (adminProfile is null)
        {
            return null;
        }

        var gender = adminProfile.Gender;

        return new UserProfileResult(
            adminProfile.FirstName,
            adminProfile.LastName,
            gender,
            adminProfile.Email,
            adminProfile.Address ?? "",
            adminProfile.Avatar ?? Array.Empty<byte>(),
            adminProfile.PhoneNumber ?? ""
        );
    }

    public async Task<Customer?> RetrieveUser(Guid userId)
    {
        var customer = await _dbContext.Customers
            .Where(u => u.Id == CustomerId.Create(userId)) // Check if UserId.Create is necessary
            .FirstOrDefaultAsync();

        return customer;
    }

    public async Task UpdateCustomerStatusAsync(Customer customer)
    {
        _dbContext.Entry(customer).State = EntityState.Modified;
        // Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckUserRoleExist(Guid userRoleId)
    {
        var checkUserRole = await _dbContext.UserRoles
            .AnyAsync(o => o.Id == UserRoleId.Create(userRoleId));

        return checkUserRole;
    }

}
