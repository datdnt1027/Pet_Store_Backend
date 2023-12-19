using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(TableKey.Orders);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName(TableKey.OrderId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => OrderId.Create(value)
                );

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(o => o.OrderStatus)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(o => o.PaymentStatus)
                .IsRequired()
                .HasConversion<string>();

            // Configure the ExpectedDelivery value object
            builder.OwnsOne(o => o.ExpectedDelivery, ed =>
            {
                ed.Property(d => d.StartDate)
                    .HasColumnName("ExpectedDeliveryStartDate");

                ed.Property(d => d.EndDate)
                    .HasColumnName("ExpectedDeliveryEndDate");
            });
        }

    }
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable(TableKey.OrderProducts);

            builder.HasKey(op => op.Id);

            builder.Property(op => op.Id)
                .ValueGeneratedNever()
                .HasColumnName(TableKey.OrderProductId)
                .HasConversion(
                    id => id.Value,
                    value => OrderProductId.Create(value)
                );

            builder.Property(op => op.Quantity)
                .IsRequired();

            builder.Property(op => op.OrderProductStatus)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .IsRequired(false);
        }
    }

}
