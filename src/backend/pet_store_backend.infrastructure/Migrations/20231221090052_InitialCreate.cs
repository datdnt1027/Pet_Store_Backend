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
                    { new Guid("6a3680fc-348e-4170-adb1-e0c1eeece06a"), true, "Sale" },
                    { new Guid("7e021ad2-1211-4a89-b923-9df6ede50227"), true, "User" },
                    { new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a"), true, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Avatar", "CustomerRoleId", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("94ce6dcd-dbb8-4be4-91ad-12cb0114ed94"), null, null, new Guid("7e021ad2-1211-4a89-b923-9df6ede50227"), "20110629@student.hcmute.edu.vn", "Dat", null, "Thien", new byte[] { 94, 237, 240, 239, 67, 215, 224, 80, 250, 151, 92, 136, 90, 117, 22, 81, 188, 210, 15, 102, 35, 87, 213, 16, 64, 223, 221, 214, 183, 39, 68, 45, 211, 239, 104, 35, 235, 229, 188, 187, 161, 129, 73, 138, 206, 192, 33, 58, 13, 124, 97, 71, 164, 156, 52, 37, 238, 6, 17, 66, 13, 227, 73, 166 }, null, new byte[] { 240, 206, 73, 93, 122, 238, 68, 224, 103, 32, 168, 170, 22, 97, 0, 168, 76, 170, 73, 69, 2, 41, 213, 39, 12, 214, 109, 160, 69, 86, 108, 56, 73, 218, 5, 9, 55, 1, 119, 121, 134, 150, 12, 95, 234, 230, 221, 241, 248, 154, 229, 68, 12, 231, 163, 170, 98, 250, 54, 171, 233, 139, 32, 219, 185, 91, 228, 12, 211, 47, 143, 245, 254, 187, 220, 219, 227, 17, 125, 119, 23, 4, 222, 240, 200, 232, 38, 104, 91, 209, 151, 184, 83, 55, 94, 84, 36, 77, 196, 75, 71, 242, 52, 122, 204, 140, 190, 213, 7, 252, 124, 5, 69, 154, 191, 231, 237, 123, 230, 62, 28, 207, 72, 17, 70, 167, 237, 34 }, null, true, null, null, new DateTime(2023, 12, 21, 16, 0, 52, 169, DateTimeKind.Local).AddTicks(7873) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("0d6d0663-0570-4dec-8652-0012457e1b99"), true, true, true, "Orders", true, new Guid("6a3680fc-348e-4170-adb1-e0c1eeece06a") },
                    { new Guid("1519c49d-3675-4ecd-bc64-a9fb735dbaf0"), true, true, true, "Categories", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("344178b4-e1ff-4770-bdf0-4e5f4e8fe832"), true, false, true, "Categories", false, new Guid("6a3680fc-348e-4170-adb1-e0c1eeece06a") },
                    { new Guid("5e2e281b-60ba-4e2e-991e-9a9b81297fe0"), true, false, true, "Products", false, new Guid("6a3680fc-348e-4170-adb1-e0c1eeece06a") },
                    { new Guid("6481e615-f78c-4999-a0f6-675cd159f547"), true, true, true, "UserPermissions", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("673f60a9-4520-4f60-9a5b-7611c2ed9a1b"), true, true, true, "UserRoles", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("6bb6047e-71e0-466a-8e74-91d10a961672"), true, true, true, "OrderProducts", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("8f513fe1-ac03-4c51-b969-9450463a33f9"), true, true, true, "Customers", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("a2921b51-37ed-4792-8baa-c4e8786e9084"), true, true, true, "Products", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("bedd2ec6-75d3-4b1f-97e4-07458238d5c6"), true, true, true, "Orders", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") },
                    { new Guid("d1699c73-ca18-43f9-ba19-76f5083722b1"), true, true, true, "Users", true, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("d5fb6bb2-0698-4cd7-9dc7-cd4c025c674f"), null, null, "dntdat09@gmail.com", "Dat", null, "Thien", new byte[] { 72, 69, 48, 103, 131, 76, 54, 118, 95, 245, 91, 229, 2, 87, 121, 21, 138, 151, 61, 131, 43, 17, 199, 29, 46, 147, 185, 154, 237, 57, 89, 233, 187, 231, 72, 226, 198, 233, 68, 241, 36, 80, 118, 135, 3, 76, 82, 218, 51, 111, 73, 7, 55, 120, 158, 131, 154, 86, 255, 122, 159, 151, 187, 218 }, null, new byte[] { 41, 212, 2, 180, 247, 250, 9, 19, 85, 94, 70, 8, 122, 235, 14, 249, 217, 85, 194, 40, 41, 59, 150, 244, 92, 139, 149, 101, 99, 166, 227, 236, 108, 224, 114, 34, 11, 133, 43, 230, 47, 139, 253, 198, 33, 140, 235, 237, 84, 131, 231, 94, 100, 19, 16, 149, 30, 30, 12, 15, 84, 123, 154, 159, 3, 130, 153, 212, 222, 46, 69, 248, 203, 28, 20, 180, 58, 34, 38, 210, 208, 50, 128, 97, 64, 12, 109, 222, 221, 160, 211, 49, 235, 216, 181, 41, 70, 194, 244, 232, 72, 238, 35, 128, 191, 174, 158, 210, 155, 209, 41, 21, 124, 117, 251, 11, 65, 102, 44, 47, 11, 178, 103, 255, 202, 30, 97, 251 }, null, true, null, new Guid("faf4fd87-92ea-43ee-bfe0-e034e5025a4a"), null, new DateTime(2023, 12, 21, 16, 0, 52, 169, DateTimeKind.Local).AddTicks(6736) });

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
