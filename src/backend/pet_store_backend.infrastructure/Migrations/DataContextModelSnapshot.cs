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
                        .IsRequired()
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
                            Id = new Guid("457d7cd7-78bb-4950-af17-2bc1bf8388ee"),
                            CustomerRoleId = new Guid("44139d0e-cbeb-4590-8fa9-6e07f6cd0045"),
                            Email = "20110629@student.hcmute.edu.vn",
                            FirstName = "Dat",
                            LastName = "Thien",
                            PasswordHash = new byte[] { 175, 71, 165, 170, 232, 246, 216, 129, 49, 236, 187, 240, 136, 243, 235, 188, 126, 160, 246, 168, 241, 230, 5, 151, 89, 7, 13, 4, 164, 193, 189, 185, 143, 50, 222, 108, 195, 255, 210, 196, 208, 216, 181, 231, 207, 29, 168, 200, 205, 93, 56, 117, 176, 234, 166, 18, 248, 230, 18, 118, 62, 211, 41, 197 },
                            PasswordSalt = new byte[] { 22, 206, 54, 220, 239, 63, 113, 165, 214, 9, 75, 56, 47, 225, 18, 107, 122, 51, 130, 109, 134, 54, 87, 167, 46, 63, 162, 128, 78, 203, 94, 218, 42, 220, 63, 48, 136, 136, 86, 10, 198, 207, 118, 97, 232, 15, 224, 152, 65, 113, 204, 254, 58, 163, 24, 151, 49, 159, 175, 170, 134, 156, 248, 28, 70, 250, 102, 29, 232, 189, 20, 188, 96, 102, 70, 5, 19, 148, 252, 27, 224, 108, 212, 70, 254, 99, 25, 122, 25, 88, 122, 242, 193, 43, 255, 111, 8, 44, 187, 121, 129, 128, 178, 184, 149, 161, 240, 19, 220, 155, 32, 217, 59, 219, 228, 23, 152, 30, 247, 1, 100, 41, 202, 74, 193, 133, 243, 51 },
                            Status = true,
                            VerifiedAt = new DateTime(2023, 12, 21, 17, 8, 1, 691, DateTimeKind.Local).AddTicks(4)
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
                            Id = new Guid("f2327ead-47fc-466f-a2de-2d402b7eea4e"),
                            Email = "dntdat09@gmail.com",
                            FirstName = "Dat",
                            LastName = "Thien",
                            PasswordHash = new byte[] { 247, 103, 56, 251, 61, 137, 175, 162, 120, 99, 164, 68, 38, 45, 170, 199, 201, 95, 24, 0, 32, 170, 224, 223, 192, 224, 122, 78, 151, 136, 34, 202, 118, 217, 65, 217, 226, 162, 84, 229, 152, 16, 109, 78, 224, 29, 126, 51, 124, 24, 79, 220, 136, 94, 30, 65, 236, 168, 14, 64, 155, 149, 222, 202 },
                            PasswordSalt = new byte[] { 171, 190, 203, 115, 52, 13, 189, 87, 228, 80, 24, 42, 223, 241, 247, 117, 242, 50, 113, 57, 46, 75, 227, 148, 251, 221, 47, 19, 173, 242, 228, 179, 159, 23, 82, 57, 198, 76, 227, 175, 68, 143, 37, 145, 139, 87, 83, 37, 61, 132, 20, 98, 237, 218, 114, 197, 160, 229, 221, 36, 52, 131, 85, 158, 127, 228, 238, 146, 118, 27, 85, 124, 86, 128, 208, 81, 27, 74, 154, 155, 233, 244, 162, 153, 6, 108, 241, 160, 102, 171, 165, 40, 41, 106, 216, 127, 171, 37, 177, 222, 127, 182, 0, 245, 3, 107, 165, 136, 251, 109, 76, 54, 82, 193, 44, 22, 177, 228, 98, 200, 67, 204, 200, 122, 134, 88, 184, 89 },
                            Status = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a"),
                            VerifiedAt = new DateTime(2023, 12, 21, 17, 8, 1, 690, DateTimeKind.Local).AddTicks(9501)
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
                            Id = new Guid("04b3b37e-ad49-4cd1-bade-ab5908e30225"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Users",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("06e89709-3e4b-452c-9030-94c7c815225e"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Customers",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("447d625b-f11b-4918-b78a-145ad74f2ab0"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Categories",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("f4d02afd-4518-407e-a97a-321669c65f7c"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Products",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("c1de1912-7991-4d61-a017-baea003fd4ce"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "UserRoles",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("1b4bee5d-6c8b-487d-9ea5-72feca8e6921"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "UserPermissions",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("1918baba-80e8-46b9-beb7-478f486a7d20"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Orders",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("aca04a18-8fe2-4de7-8b6e-e091f4f50afa"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "OrderProducts",
                            Update = true,
                            UserRoleId = new Guid("b80b5e31-ba19-4144-8115-20804c79586a")
                        },
                        new
                        {
                            Id = new Guid("adf41f27-c3d0-4211-af5f-dd718d8f15cc"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Orders",
                            Update = true,
                            UserRoleId = new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778")
                        },
                        new
                        {
                            Id = new Guid("bca6da48-f542-4ad5-825e-ae45dfd55db5"),
                            Create = true,
                            Deactive = false,
                            Read = true,
                            TableName = "Categories",
                            Update = false,
                            UserRoleId = new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778")
                        },
                        new
                        {
                            Id = new Guid("bbdfb3a8-fb6f-4471-8d02-8424979e7846"),
                            Create = true,
                            Deactive = false,
                            Read = true,
                            TableName = "Products",
                            Update = false,
                            UserRoleId = new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778")
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
                            Id = new Guid("b80b5e31-ba19-4144-8115-20804c79586a"),
                            Status = true,
                            UserRoleName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778"),
                            Status = true,
                            UserRoleName = "Sale"
                        },
                        new
                        {
                            Id = new Guid("44139d0e-cbeb-4590-8fa9-6e07f6cd0045"),
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
