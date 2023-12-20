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
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
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
                    { new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397"), true, "Admin" },
                    { new Guid("2efbff6a-599e-4738-a619-dd2f9c9f1543"), true, "User" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Avatar", "CustomerRoleId", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("dc69ea5a-916c-4c5c-9b8d-041c6afe809d"), null, null, new Guid("2efbff6a-599e-4738-a619-dd2f9c9f1543"), "20110629@student.hcmute.edu.vn", "Dat", null, "Thien", new byte[] { 234, 127, 93, 177, 150, 250, 75, 76, 164, 228, 217, 159, 75, 76, 84, 237, 37, 147, 214, 15, 222, 164, 11, 30, 251, 61, 86, 95, 72, 41, 146, 192, 222, 39, 14, 57, 189, 250, 199, 152, 75, 57, 49, 133, 169, 219, 176, 36, 19, 105, 218, 28, 103, 87, 207, 36, 220, 65, 18, 144, 97, 153, 155, 104 }, null, new byte[] { 114, 254, 85, 47, 112, 129, 67, 176, 83, 221, 77, 222, 225, 247, 76, 56, 59, 50, 139, 223, 191, 205, 42, 216, 221, 47, 110, 63, 77, 238, 157, 223, 94, 19, 168, 76, 217, 13, 104, 100, 140, 77, 198, 187, 138, 3, 107, 124, 115, 63, 73, 220, 126, 169, 28, 89, 130, 230, 14, 158, 244, 77, 212, 166, 4, 138, 110, 116, 241, 173, 52, 103, 45, 199, 112, 25, 244, 12, 8, 0, 238, 241, 8, 34, 255, 116, 67, 227, 10, 69, 188, 232, 39, 158, 131, 40, 85, 60, 56, 26, 203, 60, 150, 24, 32, 124, 72, 143, 176, 7, 133, 207, 48, 198, 34, 120, 248, 249, 10, 92, 153, 61, 15, 69, 82, 32, 226, 51 }, null, true, null, null, new DateTime(2023, 12, 20, 19, 38, 34, 419, DateTimeKind.Local).AddTicks(776) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("0f928576-21cd-442f-bec2-98746090003a"), true, true, true, "OrderProducts", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("11400538-f5fb-4376-859c-c402d45a5132"), true, true, true, "Customers", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("19386eaf-752d-4f21-a6e5-60e32e5d0997"), true, true, true, "UserRoles", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("1eab85c6-a7e2-4384-a352-427f615201f0"), true, true, true, "Orders", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("3a837705-4133-4f01-a4fb-30e65160ecd0"), true, true, true, "Users", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("7ce3f6e9-3b31-4762-a577-0df38d388b35"), true, true, true, "Categories", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("d560bee4-0de0-4783-baa5-f98329071635"), true, true, true, "UserPermissions", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") },
                    { new Guid("e24cabee-aea1-415f-8df8-08147e5adc21"), true, true, true, "Products", true, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("74e5c1f1-a743-443a-9e3f-40b5a3a95ac9"), null, null, "dntdat09@gmail.com", "Dat", null, "Thien", new byte[] { 108, 3, 201, 52, 104, 8, 158, 155, 200, 172, 185, 10, 3, 254, 108, 221, 187, 81, 115, 167, 135, 75, 161, 24, 122, 115, 204, 140, 12, 62, 113, 92, 133, 168, 93, 251, 213, 99, 162, 236, 113, 32, 158, 30, 240, 178, 42, 61, 40, 41, 85, 202, 181, 63, 115, 178, 159, 54, 86, 245, 16, 71, 44, 136 }, null, new byte[] { 202, 217, 169, 172, 66, 55, 85, 160, 178, 230, 6, 8, 20, 77, 82, 170, 50, 51, 146, 139, 183, 149, 213, 238, 146, 145, 203, 159, 31, 148, 41, 114, 106, 87, 31, 47, 227, 247, 42, 91, 211, 241, 42, 242, 159, 225, 41, 90, 179, 125, 216, 198, 171, 24, 234, 191, 82, 62, 235, 118, 251, 155, 230, 12, 33, 174, 5, 76, 214, 27, 192, 15, 221, 132, 114, 138, 17, 108, 235, 161, 92, 137, 212, 231, 15, 104, 92, 113, 127, 158, 21, 28, 30, 90, 218, 78, 204, 163, 115, 245, 30, 62, 138, 12, 191, 244, 65, 182, 59, 195, 66, 110, 62, 254, 84, 203, 104, 235, 62, 113, 105, 5, 54, 246, 214, 151, 130, 104 }, null, true, null, new Guid("1fc87eb4-0820-40e7-bc9d-72dae05dd397"), null, new DateTime(2023, 12, 20, 19, 38, 34, 419, DateTimeKind.Local).AddTicks(491) });

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
