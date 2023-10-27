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

        private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
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
                .IsRequired(); // Đánh dấu là bắt buộc

            builder.Property(m => m.Password)
                .HasMaxLength(255)
                .IsRequired(); // Đánh dấu là bắt buộc
        }
    }
}
