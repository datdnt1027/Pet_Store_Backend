﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pet_store_backend.infrastructure.Persistence;

#nullable disable

namespace pet_store_backend.infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("pet_store_backend.domain.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderId");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Orders.OrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderProductId");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderProductStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts", (string)null);
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.PetProducts.PetProduct.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProductDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.PetProducts.PetProductCategory.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CategoryId");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CustomerId");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("CustomerRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("datetime");

                    b.Property<string>("VerificationToken")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CustomerRoleId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PasswordResetToken")
                        .IsUnique()
                        .HasFilter("[PasswordResetToken] IS NOT NULL");

                    b.HasIndex("VerificationToken")
                        .IsUnique()
                        .HasFilter("[VerificationToken] IS NOT NULL");

                    b.ToTable("Customers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("50bad5c8-1ede-45c3-8d18-b4eab903c0b3"),
                            CustomerRoleId = new Guid("df724f14-efcc-43ba-9f33-1facb5e66dad"),
                            Email = "20110629@student.hcmute.edu.vn",
                            FirstName = "Dat",
                            LastName = "Thien",
                            PasswordHash = new byte[] { 148, 253, 138, 120, 0, 203, 113, 25, 132, 47, 30, 157, 213, 42, 250, 240, 216, 119, 163, 45, 160, 212, 48, 208, 75, 4, 27, 255, 235, 145, 203, 38, 241, 0, 17, 237, 118, 58, 227, 251, 79, 34, 173, 120, 107, 168, 44, 166, 77, 4, 136, 8, 26, 45, 96, 68, 242, 122, 44, 249, 124, 118, 70, 108 },
                            PasswordSalt = new byte[] { 58, 198, 239, 86, 146, 220, 157, 89, 41, 128, 15, 170, 137, 231, 254, 8, 173, 137, 255, 128, 134, 119, 250, 189, 187, 189, 255, 78, 159, 127, 177, 213, 5, 64, 177, 160, 156, 49, 114, 11, 78, 41, 131, 157, 136, 99, 184, 42, 194, 66, 196, 217, 64, 209, 66, 246, 141, 143, 70, 11, 63, 241, 183, 81, 160, 5, 94, 124, 29, 157, 98, 36, 177, 254, 200, 118, 120, 41, 42, 105, 123, 222, 218, 19, 41, 210, 170, 219, 49, 29, 204, 186, 53, 133, 11, 240, 89, 248, 247, 168, 125, 106, 36, 114, 115, 169, 33, 126, 8, 148, 50, 80, 240, 168, 7, 23, 209, 149, 89, 251, 201, 115, 147, 62, 183, 160, 75, 66 },
                            Status = true,
                            VerifiedAt = new DateTime(2023, 12, 19, 14, 12, 34, 985, DateTimeKind.Local).AddTicks(2496)
                        });
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("UserRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VerificationToken")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PasswordResetToken")
                        .IsUnique()
                        .HasFilter("[PasswordResetToken] IS NOT NULL");

                    b.HasIndex("UserRoleId");

                    b.HasIndex("VerificationToken")
                        .IsUnique()
                        .HasFilter("[VerificationToken] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0a4cca38-196e-426c-8770-b76c164d60a1"),
                            Email = "dntdat09@gmail.com",
                            FirstName = "Dat",
                            LastName = "Thien",
                            PasswordHash = new byte[] { 222, 217, 40, 173, 131, 3, 65, 119, 208, 92, 233, 44, 33, 229, 22, 198, 40, 64, 48, 236, 145, 139, 202, 194, 144, 78, 192, 53, 166, 168, 135, 122, 107, 10, 206, 253, 248, 3, 114, 129, 235, 155, 78, 210, 187, 106, 75, 215, 247, 110, 114, 157, 159, 115, 151, 100, 30, 23, 117, 200, 175, 115, 183, 89 },
                            PasswordSalt = new byte[] { 111, 181, 19, 89, 27, 86, 132, 0, 76, 238, 38, 4, 223, 208, 143, 204, 93, 171, 171, 130, 101, 106, 228, 98, 250, 207, 91, 142, 199, 242, 63, 180, 242, 175, 63, 87, 187, 183, 53, 253, 5, 109, 110, 230, 166, 150, 157, 206, 88, 174, 76, 26, 68, 210, 253, 125, 151, 76, 41, 171, 92, 217, 20, 20, 6, 6, 215, 53, 31, 45, 145, 175, 47, 220, 45, 27, 243, 223, 197, 38, 118, 215, 139, 5, 81, 136, 191, 192, 134, 228, 27, 84, 217, 196, 114, 141, 13, 220, 171, 45, 64, 35, 120, 114, 73, 170, 152, 236, 244, 231, 22, 104, 59, 200, 133, 30, 186, 149, 150, 157, 126, 29, 204, 24, 0, 174, 49, 145 },
                            Status = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5"),
                            VerifiedAt = new DateTime(2023, 12, 19, 14, 12, 34, 985, DateTimeKind.Local).AddTicks(2197)
                        });
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.UserPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserPermissionId");

                    b.Property<bool>("Create")
                        .HasColumnType("bit");

                    b.Property<bool>("Deactive")
                        .HasColumnType("bit");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Update")
                        .HasColumnType("bit");

                    b.Property<Guid?>("UserRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserPermissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4d28b508-1fca-4979-97db-78cdb16b97ed"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Users",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("3d24c76b-2bb0-4fe4-b0b5-8102358c5433"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Customers",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("02349c08-9cf7-484f-80f0-a68c486f5def"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Categories",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("5b9ac138-a77b-4e50-9560-284a58784fec"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Products",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("3ca86f6d-534e-4c7c-a3d7-127137e943c4"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "UserRoles",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("feea1459-d55c-4f85-816a-662ee782fac0"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "UserPermissions",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("972cf35e-72c9-4360-b689-8b2e83efafc2"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Orders",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        },
                        new
                        {
                            Id = new Guid("57189b68-310e-4ad1-aea0-14fa94d9bc65"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "OrderProducts",
                            Update = true,
                            UserRoleId = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5")
                        });
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserRoleId");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserRoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5dc6ac1d-c750-44f8-bfb0-7acbb9f733f5"),
                            Status = true,
                            UserRoleName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("df724f14-efcc-43ba-9f33-1facb5e66dad"),
                            Status = true,
                            UserRoleName = "User"
                        });
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Orders.Order", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.Users.User", "User")
                        .WithMany("UserProducts")
                        .HasForeignKey("UserId");

                    b.OwnsOne("pet_store_backend.domain.Entities.Orders.ValueObjects.DeliveryDate", "ExpectedDelivery", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("ExpectedDeliveryEndDate");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("ExpectedDeliveryStartDate");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("ExpectedDelivery");

                    b.Navigation("User");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Orders.OrderProduct", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.Users.Customer", "Customer")
                        .WithMany("CustomerProducts")
                        .HasForeignKey("CustomerId");

                    b.HasOne("pet_store_backend.domain.Entities.Orders.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId");

                    b.HasOne("pet_store_backend.domain.Entities.PetProducts.PetProduct.Product", "Product")
                        .WithMany("UserProducts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Customer");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.PetProducts.PetProduct.Product", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.PetProducts.PetProductCategory.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("pet_store_backend.domain.Entities.PetProducts.ValueObjects.Price", "ProductPrice", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18, 2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.Property<decimal>("Discount")
                                .HasColumnType("decimal(18, 2)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("ProductPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.Customer", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.Users.UserRole", "CustomerRole")
                        .WithMany("Customers")
                        .HasForeignKey("CustomerRoleId");

                    b.Navigation("CustomerRole");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.User", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.Users.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.UserPermission", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.Users.UserRole", "UserRole")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserRoleId");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Orders.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.PetProducts.PetProduct.Product", b =>
                {
                    b.Navigation("UserProducts");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.PetProducts.PetProductCategory.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.Customer", b =>
                {
                    b.Navigation("CustomerProducts");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.User", b =>
                {
                    b.Navigation("UserProducts");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.Users.UserRole", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("UserPermissions");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
