﻿using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence.Repositories
{
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

        //Update information for Customer
        public async Task Update(Customer customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();
        }

        // This is User Email
        public async Task<UserRole?> GetUserByEmail(string email)
        {
            var userRole = await _dbContext.UserRoles
                .Where(ur => ur.User.Email == email)
                .Include(ur => ur.User)
                .FirstOrDefaultAsync();

            return userRole;
        }

        // This is Customer Email
        public async Task<UserRole?> GetCustomerByEmail(string email)
        {
            var customerRole = await _dbContext.UserRoles
                .Where(ur => ur.Customer.Email == email)
                .Include(ur => ur.Customer)
                .FirstOrDefaultAsync();

            return customerRole;
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
    }
}
