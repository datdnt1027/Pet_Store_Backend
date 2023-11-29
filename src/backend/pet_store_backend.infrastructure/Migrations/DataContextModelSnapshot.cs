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

            modelBuilder.Entity("pet_store_backend.domain.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

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

                    b.HasIndex("UserRoleId")
                        .IsUnique()
                        .HasFilter("[UserRoleId] IS NOT NULL");

                    b.HasIndex("VerificationToken")
                        .IsUnique()
                        .HasFilter("[VerificationToken] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2f68514a-9fc6-4df4-98bb-b957149bba83"),
                            Email = "dntdat09@gmail.com",
                            FirstName = "Dat",
                            LastName = "Thien",
                            PasswordHash = new byte[] { 118, 72, 136, 234, 190, 194, 76, 66, 162, 237, 64, 32, 8, 245, 255, 132, 52, 211, 19, 5, 44, 82, 120, 61, 116, 45, 213, 254, 126, 233, 12, 248, 252, 105, 4, 190, 198, 188, 230, 140, 214, 40, 192, 223, 105, 249, 62, 88, 161, 108, 68, 76, 253, 179, 183, 117, 172, 12, 150, 218, 20, 188, 242, 5 },
                            PasswordSalt = new byte[] { 200, 131, 170, 132, 209, 192, 174, 149, 107, 207, 69, 112, 92, 1, 23, 44, 137, 197, 148, 211, 211, 45, 84, 169, 132, 254, 157, 0, 209, 221, 138, 49, 218, 193, 54, 36, 32, 151, 208, 156, 52, 189, 184, 180, 110, 134, 91, 72, 149, 160, 248, 32, 11, 217, 201, 195, 139, 217, 197, 114, 114, 128, 157, 67, 24, 46, 26, 246, 189, 4, 228, 2, 161, 131, 85, 50, 209, 188, 161, 34, 248, 48, 161, 32, 81, 153, 245, 104, 63, 37, 215, 64, 77, 188, 10, 5, 105, 85, 103, 159, 62, 153, 0, 42, 48, 242, 78, 208, 241, 249, 70, 133, 128, 151, 12, 197, 81, 143, 220, 41, 30, 165, 20, 125, 198, 234, 162, 203 },
                            UserRoleId = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0"),
                            VerifiedAt = new DateTime(2023, 11, 25, 12, 12, 29, 687, DateTimeKind.Local).AddTicks(71)
                        });
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.User.UserPermission", b =>
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

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserPermissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3d5f15b8-ced3-4d87-b6d4-b61331ec1458"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Users",
                            Update = true,
                            UserRoleId = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0")
                        },
                        new
                        {
                            Id = new Guid("5d58ce28-eff0-488e-9b4b-c92ce7e899bf"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Categories",
                            Update = true,
                            UserRoleId = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0")
                        },
                        new
                        {
                            Id = new Guid("6f04a512-bf01-4870-87f8-b3a6b877ffa9"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "Products",
                            Update = true,
                            UserRoleId = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0")
                        },
                        new
                        {
                            Id = new Guid("b4197904-856d-40af-a71d-cebe00e2c960"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "UserRoles",
                            Update = true,
                            UserRoleId = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0")
                        },
                        new
                        {
                            Id = new Guid("7228ee6a-cdec-465e-9410-1ee481c503d2"),
                            Create = true,
                            Deactive = true,
                            Read = true,
                            TableName = "UserPermissions",
                            Update = true,
                            UserRoleId = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0")
                        },
                        new
                        {
                            Id = new Guid("84fcf437-b1e2-49ce-ba1b-acd9dda98846"),
                            Create = true,
                            Deactive = false,
                            Read = false,
                            TableName = "Products",
                            Update = false,
                            UserRoleId = new Guid("14227c76-a4f0-434d-bdbf-0f72b38a36b4")
                        });
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.User.UserRole", b =>
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
                            Id = new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0"),
                            Status = true,
                            UserRoleName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("14227c76-a4f0-434d-bdbf-0f72b38a36b4"),
                            Status = true,
                            UserRoleName = "User"
                        });
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

            modelBuilder.Entity("pet_store_backend.domain.Entities.User.User", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.User.UserRole", null)
                        .WithOne("User")
                        .HasForeignKey("pet_store_backend.domain.Entities.User.User", "UserRoleId");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.User.UserPermission", b =>
                {
                    b.HasOne("pet_store_backend.domain.Entities.User.UserRole", "UserRole")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.PetProducts.PetProductCategory.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("pet_store_backend.domain.Entities.User.UserRole", b =>
                {
                    b.Navigation("User")
                        .IsRequired();

                    b.Navigation("UserPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
