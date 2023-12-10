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
                    { new Guid("9d329fdc-68ee-488a-addb-ede9a1027116"), true, "Admin" },
                    { new Guid("d9ad1cf2-04d4-4fa6-b286-b222887f13cc"), true, "User" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Avatar", "CustomerRoleId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("9a451229-0400-4ee2-a059-6262884f47e8"), null, null, new Guid("d9ad1cf2-04d4-4fa6-b286-b222887f13cc"), "20110629@student.hcmute.edu.vn", "Dat", "Thien", new byte[] { 92, 40, 34, 116, 102, 246, 238, 211, 228, 15, 223, 99, 179, 85, 7, 164, 105, 40, 199, 96, 98, 245, 124, 68, 176, 75, 165, 189, 141, 76, 21, 24, 162, 213, 224, 159, 1, 126, 201, 247, 245, 129, 241, 110, 181, 59, 176, 159, 90, 49, 150, 9, 81, 92, 202, 182, 137, 138, 22, 150, 191, 112, 179, 46 }, null, new byte[] { 150, 153, 131, 190, 27, 164, 169, 79, 153, 123, 28, 66, 182, 196, 184, 39, 27, 61, 7, 110, 189, 242, 217, 12, 33, 235, 168, 56, 227, 20, 199, 142, 172, 35, 70, 180, 103, 227, 210, 104, 155, 253, 80, 4, 51, 179, 192, 54, 111, 4, 100, 47, 246, 242, 191, 209, 70, 5, 200, 138, 10, 229, 182, 106, 241, 230, 50, 3, 209, 66, 42, 190, 40, 189, 189, 126, 49, 92, 225, 110, 90, 221, 140, 112, 13, 64, 117, 49, 162, 219, 209, 41, 42, 96, 5, 72, 24, 182, 162, 163, 181, 182, 116, 146, 188, 48, 219, 226, 190, 223, 28, 11, 122, 89, 212, 126, 160, 81, 165, 219, 169, 167, 145, 62, 43, 206, 54, 88 }, null, true, null, null, new DateTime(2023, 12, 10, 16, 0, 9, 935, DateTimeKind.Local).AddTicks(8455) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("48d3ed6f-56b5-421b-8d68-f829475745b9"), true, true, true, "Users", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("559d3c3a-d4f8-4bf7-955c-ce2601e301d8"), true, true, true, "UserPermissions", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("58845ea0-ea05-4b15-82d7-c2006f16b66a"), true, true, true, "OrderProducts", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("7d6ce668-9650-4769-8f96-e3f62ed250aa"), true, true, true, "Categories", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("b5aaa48c-06e2-446c-af72-fa5ed14bb82b"), true, true, true, "Orders", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("d198b7d1-42a5-45e7-971f-7da94ae099a6"), true, true, true, "Products", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("d41b63e7-d8b9-42dc-92bb-3034ca8218e9"), true, true, true, "Customers", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") },
                    { new Guid("d5f4fa8c-8cd9-4fac-802e-0fc10b98b39e"), true, true, true, "UserRoles", true, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("88995bc2-992d-45bb-a35f-74438fb97f76"), null, null, "dntdat09@gmail.com", "Dat", "Thien", new byte[] { 71, 55, 249, 100, 245, 244, 87, 192, 183, 238, 236, 48, 172, 92, 176, 234, 180, 124, 137, 181, 243, 102, 16, 177, 133, 120, 216, 206, 132, 43, 92, 215, 3, 77, 38, 245, 168, 57, 133, 160, 72, 13, 116, 236, 186, 143, 233, 199, 193, 141, 9, 29, 71, 248, 18, 206, 14, 223, 100, 10, 209, 161, 27, 44 }, null, new byte[] { 50, 95, 35, 24, 180, 143, 44, 0, 77, 63, 169, 154, 109, 76, 126, 87, 231, 105, 80, 134, 61, 114, 182, 116, 229, 175, 34, 173, 66, 223, 246, 18, 207, 118, 234, 242, 213, 37, 242, 190, 3, 210, 37, 233, 206, 75, 234, 214, 223, 137, 32, 176, 183, 9, 74, 118, 115, 74, 41, 162, 25, 243, 233, 53, 216, 64, 252, 27, 44, 120, 87, 60, 93, 181, 122, 203, 163, 240, 130, 251, 154, 62, 147, 58, 15, 247, 190, 145, 112, 40, 247, 77, 244, 147, 253, 170, 248, 64, 86, 123, 44, 62, 91, 102, 230, 72, 130, 95, 119, 233, 70, 255, 207, 28, 105, 81, 8, 94, 188, 237, 180, 248, 232, 109, 35, 29, 219, 234 }, null, true, null, new Guid("9d329fdc-68ee-488a-addb-ede9a1027116"), null, new DateTime(2023, 12, 10, 16, 0, 9, 935, DateTimeKind.Local).AddTicks(7243) });

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
