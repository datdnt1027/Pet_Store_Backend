using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence.Configurations
{
    public class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            ConfigureUserRolesTable(builder);
        }

        private static void ConfigureUserRolesTable(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(TableKey.UserRoles);

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasColumnName(TableKey.UserRoleId) // Specify the column name
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserRoleId.Create(value));

            builder.Property(r => r.UserRoleName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.Status)
                .HasColumnType("bit")
                .IsRequired();

            // Configure the UserRoleId as a foreign key to UserRole
            builder.HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<User>(u => u.UserRoleId)
                .IsRequired(false);

            // Configure the UserRoleId as a foreign key to UserRole
            builder.HasOne(r => r.Customer)
                .WithOne()
                .HasForeignKey<Customer>(u => u.CustomerRoleId)
                .IsRequired(false);

            builder.HasMany(r => r.UserPermissions)
                .WithOne(r => r.UserRole)
                .HasForeignKey(r => r.UserRoleId)
                .IsRequired(false);
        }
    }

    public class UserPermissionConfiguraions : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            ConfigureUserPermissions(builder);
        }

        private static void ConfigureUserPermissions(EntityTypeBuilder<UserPermission> builder)
        {

            builder.ToTable(TableKey.UserPermissions);

            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasColumnName(TableKey.UserPermissionId)
                .HasConversion(
                    id => id.Value,
                    value => UserPermissionId.Create(value)
            );

            builder.Property(p => p.TableName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Create)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(p => p.Read)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(p => p.Update)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(p => p.Deactive)
                .HasColumnType("bit")
                .IsRequired();

        }
    }

    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
        }

        private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableKey.Users);

            builder.HasKey(builder => builder.Id);
            builder.Property(m => m.Id)
                .HasColumnName(TableKey.UserId) // Specify the column name
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
            );

            builder.Property(m => m.FirstName)
                .HasMaxLength(200);

            builder.Property(m => m.LastName)
                .HasMaxLength(200);

            builder.Property(m => m.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(m => m.Email)
                .IsUnique();

            builder.Property(m => m.PasswordHash)
                .IsRequired();

            builder.Property(m => m.PasswordSalt)
                .IsRequired();

            builder.Property(m => m.VerificationToken)
                .HasMaxLength(255);

            builder.HasIndex(m => m.VerificationToken)
                .IsUnique();

            builder.Property(m => m.VerifiedAt)
                .HasColumnType("datetime");

            builder.Property(m => m.PasswordResetToken)
                .HasMaxLength(255);

            builder.HasIndex(m => m.PasswordResetToken)
                .IsUnique();

            builder.Property(m => m.TokenExpires)
                .HasColumnType("datetime");

            builder.HasMany(r => r.UserProducts)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .IsRequired(false);
        }
    }

    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            ConfigureCustomersTable(builder);
        }

        private static void ConfigureCustomersTable(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(TableKey.Customers);

            builder.HasKey(builder => builder.Id);
            builder.Property(m => m.Id)
                .HasColumnName(TableKey.CustomerId) // Specify the column name
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CustomerId.Create(value)
            );

            builder.Property(m => m.FirstName)
                .HasMaxLength(200);

            builder.Property(m => m.LastName)
                .HasMaxLength(200);

            builder.Property(m => m.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(m => m.Email)
                .IsUnique();

            builder.Property(m => m.PasswordHash)
                .IsRequired();

            builder.Property(m => m.PasswordSalt)
                .IsRequired();

            builder.Property(m => m.VerificationToken)
                .HasMaxLength(255);

            builder.HasIndex(m => m.VerificationToken)
                .IsUnique();

            builder.Property(m => m.VerifiedAt)
                .HasColumnType("datetime");

            builder.Property(m => m.PasswordResetToken)
                .HasMaxLength(255);

            builder.HasIndex(m => m.PasswordResetToken)
                .IsUnique();

            builder.Property(m => m.TokenExpires)
                .HasColumnType("datetime");

            builder.HasMany(m => m.CustomerProducts)
                .WithOne(m => m.Customer)
                .HasForeignKey(m => m.CustomerId)
                .IsRequired(false);
        }

    }

}
