using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pet_store_backend.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductQuantity = table.Column<int>(type: "integer", nullable: false),
                    ProductPrice_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductPrice_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ProductPrice_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: true),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_UserRoles_CustomerRoleId",
                        column: x => x.CustomerRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleId");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Create = table.Column<bool>(type: "bit", nullable: false),
                    Update = table.Column<bool>(type: "bit", nullable: false),
                    Deactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.UserPermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermissions_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: true),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedDeliveryStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedDeliveryEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderProductStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.OrderProductId);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "Status", "UserRoleName" },
                values: new object[,]
                {
                    { new Guid("44139d0e-cbeb-4590-8fa9-6e07f6cd0045"), true, "User" },
                    { new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778"), true, "Sale" },
                    { new Guid("b80b5e31-ba19-4144-8115-20804c79586a"), true, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Avatar", "CustomerRoleId", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("457d7cd7-78bb-4950-af17-2bc1bf8388ee"), null, null, new Guid("44139d0e-cbeb-4590-8fa9-6e07f6cd0045"), "20110629@student.hcmute.edu.vn", "Dat", null, "Thien", new byte[] { 175, 71, 165, 170, 232, 246, 216, 129, 49, 236, 187, 240, 136, 243, 235, 188, 126, 160, 246, 168, 241, 230, 5, 151, 89, 7, 13, 4, 164, 193, 189, 185, 143, 50, 222, 108, 195, 255, 210, 196, 208, 216, 181, 231, 207, 29, 168, 200, 205, 93, 56, 117, 176, 234, 166, 18, 248, 230, 18, 118, 62, 211, 41, 197 }, null, new byte[] { 22, 206, 54, 220, 239, 63, 113, 165, 214, 9, 75, 56, 47, 225, 18, 107, 122, 51, 130, 109, 134, 54, 87, 167, 46, 63, 162, 128, 78, 203, 94, 218, 42, 220, 63, 48, 136, 136, 86, 10, 198, 207, 118, 97, 232, 15, 224, 152, 65, 113, 204, 254, 58, 163, 24, 151, 49, 159, 175, 170, 134, 156, 248, 28, 70, 250, 102, 29, 232, 189, 20, 188, 96, 102, 70, 5, 19, 148, 252, 27, 224, 108, 212, 70, 254, 99, 25, 122, 25, 88, 122, 242, 193, 43, 255, 111, 8, 44, 187, 121, 129, 128, 178, 184, 149, 161, 240, 19, 220, 155, 32, 217, 59, 219, 228, 23, 152, 30, 247, 1, 100, 41, 202, 74, 193, 133, 243, 51 }, null, true, null, null, new DateTime(2023, 12, 21, 17, 8, 1, 691, DateTimeKind.Local).AddTicks(4) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("04b3b37e-ad49-4cd1-bade-ab5908e30225"), true, true, true, "Users", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("06e89709-3e4b-452c-9030-94c7c815225e"), true, true, true, "Customers", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("1918baba-80e8-46b9-beb7-478f486a7d20"), true, true, true, "Orders", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("1b4bee5d-6c8b-487d-9ea5-72feca8e6921"), true, true, true, "UserPermissions", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("447d625b-f11b-4918-b78a-145ad74f2ab0"), true, true, true, "Categories", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("aca04a18-8fe2-4de7-8b6e-e091f4f50afa"), true, true, true, "OrderProducts", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("adf41f27-c3d0-4211-af5f-dd718d8f15cc"), true, true, true, "Orders", true, new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778") },
                    { new Guid("bbdfb3a8-fb6f-4471-8d02-8424979e7846"), true, false, true, "Products", false, new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778") },
                    { new Guid("bca6da48-f542-4ad5-825e-ae45dfd55db5"), true, false, true, "Categories", false, new Guid("7f688c2a-3df7-40a1-bfd2-517a4ff71778") },
                    { new Guid("c1de1912-7991-4d61-a017-baea003fd4ce"), true, true, true, "UserRoles", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") },
                    { new Guid("f4d02afd-4518-407e-a97a-321669c65f7c"), true, true, true, "Products", true, new Guid("b80b5e31-ba19-4144-8115-20804c79586a") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("f2327ead-47fc-466f-a2de-2d402b7eea4e"), null, null, "dntdat09@gmail.com", "Dat", null, "Thien", new byte[] { 247, 103, 56, 251, 61, 137, 175, 162, 120, 99, 164, 68, 38, 45, 170, 199, 201, 95, 24, 0, 32, 170, 224, 223, 192, 224, 122, 78, 151, 136, 34, 202, 118, 217, 65, 217, 226, 162, 84, 229, 152, 16, 109, 78, 224, 29, 126, 51, 124, 24, 79, 220, 136, 94, 30, 65, 236, 168, 14, 64, 155, 149, 222, 202 }, null, new byte[] { 171, 190, 203, 115, 52, 13, 189, 87, 228, 80, 24, 42, 223, 241, 247, 117, 242, 50, 113, 57, 46, 75, 227, 148, 251, 221, 47, 19, 173, 242, 228, 179, 159, 23, 82, 57, 198, 76, 227, 175, 68, 143, 37, 145, 139, 87, 83, 37, 61, 132, 20, 98, 237, 218, 114, 197, 160, 229, 221, 36, 52, 131, 85, 158, 127, 228, 238, 146, 118, 27, 85, 124, 86, 128, 208, 81, 27, 74, 154, 155, 233, 244, 162, 153, 6, 108, 241, 160, 102, 171, 165, 40, 41, 106, 216, 127, 171, 37, 177, 222, 127, 182, 0, 245, 3, 107, 165, 136, 251, 109, 76, 54, 82, 193, 44, 22, 177, 228, 98, 200, 67, 204, 200, 122, 134, 88, 184, 89 }, null, true, null, new Guid("b80b5e31-ba19-4144-8115-20804c79586a"), null, new DateTime(2023, 12, 21, 17, 8, 1, 690, DateTimeKind.Local).AddTicks(9501) });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerRoleId",
                table: "Customers",
                column: "CustomerRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PasswordResetToken",
                table: "Customers",
                column: "PasswordResetToken",
                unique: true,
                filter: "[PasswordResetToken] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_VerificationToken",
                table: "Customers",
                column: "VerificationToken",
                unique: true,
                filter: "[VerificationToken] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_CustomerId",
                table: "OrderProducts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserRoleId",
                table: "UserPermissions",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordResetToken",
                table: "Users",
                column: "PasswordResetToken",
                unique: true,
                filter: "[PasswordResetToken] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VerificationToken",
                table: "Users",
                column: "VerificationToken",
                unique: true,
                filter: "[VerificationToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
