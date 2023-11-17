using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;

namespace pet_store_backend.infrastructure.Persistence.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoriesTable(builder);
        ConfigureProductsTable(builder);
    }

    private static void ConfigureCategoriesTable(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value)
            ).IsRequired();

        builder.Property(c => c.CategoryName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.CreatedDateTime)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(c => c.UpdatedDateTime)
            .HasColumnType("datetime")
            .IsRequired();
    }

    private static void ConfigureProductsTable(EntityTypeBuilder<Category> builder)
    {
        builder.OwnsMany(p => p.Products, pd =>
        {
            pd.ToTable("Products");
            pd.WithOwner().HasForeignKey("CategoryId");

            pd.Property(s => s.Id)
                .ValueGeneratedNever()
                .HasColumnName("ProductId")
                .HasConversion(
                    id => id.Value,
                    value => ProductId.Create(value));

            pd.Property(p => p.ProductName)
                .HasMaxLength(100);

            pd.Property(p => p.ProductDetail)
                .HasColumnType("nvarchar(max)");

            pd.Property(p => p.ProductQuantity)
                .HasColumnType("integer");

            pd.OwnsOne(p => p.ProductPrice, pp =>
            {
                pp.Property(p => p.Discount)
                    .HasColumnType("decimal(18, 2)");

                pp.Property(p => p.Amount)
                    .HasColumnType("decimal(18, 2)");

                pp.Property(p => p.Currency)
                    .HasMaxLength(3);
            });

            pd.Property(p => p.ImageData)
                .HasColumnType("varbinary(max)");

            pd.Property(p => p.Status)
                .HasColumnName("Status")
                .HasColumnType("bit")
                .IsRequired();

            pd.Property(p => p.CreatedDateTime)
                .HasColumnType("datetime");

            pd.Property(p => p.UpdatedDateTime)
                .HasColumnType("datetime");
        });
    }
}