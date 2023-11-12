using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet_store_backend.domain.Entities.User;
using pet_store_backend.domain.Entities.User.ValueObjects;

namespace pet_store_backend.infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
        }

        private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(m => m.Id)
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
        }
    }
}
