using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoriesTable(builder);
    }

    private static void ConfigureCategoriesTable(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableKey.Categories);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasColumnName(TableKey.CategoryId)
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

        builder.HasMany(c => c.Products)
            .WithOne()
            .HasForeignKey(c => c.CategoryId)
            .IsRequired();
    }
}

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
    }

    private static void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
    {

        builder.ToTable(TableKey.Products);

        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasColumnName(TableKey.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder.Property(p => p.ProductName)
            .HasMaxLength(100);

        builder.Property(p => p.ProductDetail)
            .HasColumnType("nvarchar(max)");

        builder.Property(p => p.ProductQuantity)
            .HasColumnType("integer");

        builder.OwnsOne(p => p.ProductPrice, pp =>
        {
            pp.Property(p => p.Discount)
                .HasColumnType("decimal(18, 2)");

            pp.Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)");

            pp.Property(p => p.Currency)
                .HasMaxLength(3);
        });

        builder.Property(p => p.ImageData)
            .HasColumnType("varbinary(max)");

        builder.Property(p => p.Status)
            .HasColumnName("Status")
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(p => p.CreatedDateTime)
            .HasColumnType("datetime");

        builder.Property(p => p.UpdatedDateTime)
            .HasColumnType("datetime");
    }
}