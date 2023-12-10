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
                    { new Guid("8db33776-2124-4b04-9e2b-dec8a8e001fe"), true, "User" },
                    { new Guid("b813f428-7e93-44d1-b764-9a944da84a24"), true, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Avatar", "CustomerRoleId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("b145720e-0a2c-4c55-93b1-a787306b1b90"), null, null, new Guid("8db33776-2124-4b04-9e2b-dec8a8e001fe"), "20110629@student.hcmute.edu.vn", "Dat", "Thien", new byte[] { 79, 103, 80, 152, 44, 142, 133, 249, 47, 76, 106, 148, 128, 255, 183, 79, 18, 208, 36, 186, 148, 205, 247, 31, 161, 33, 211, 64, 168, 32, 164, 69, 91, 209, 108, 2, 150, 229, 32, 8, 115, 237, 53, 66, 174, 27, 189, 19, 15, 199, 158, 104, 115, 83, 166, 251, 224, 157, 142, 21, 180, 175, 134, 50 }, null, new byte[] { 173, 202, 239, 225, 20, 242, 199, 211, 143, 185, 76, 180, 49, 91, 225, 35, 231, 50, 235, 2, 48, 104, 17, 32, 111, 221, 50, 168, 150, 74, 152, 57, 154, 165, 35, 135, 48, 1, 175, 162, 36, 20, 110, 211, 68, 146, 213, 102, 178, 4, 112, 195, 98, 17, 212, 178, 206, 146, 126, 213, 245, 9, 184, 15, 246, 235, 220, 109, 240, 76, 177, 180, 138, 107, 75, 208, 158, 128, 11, 4, 23, 52, 119, 237, 55, 1, 133, 48, 151, 48, 63, 26, 91, 104, 91, 169, 71, 149, 74, 80, 126, 253, 219, 134, 183, 99, 133, 107, 68, 237, 226, 177, 251, 188, 216, 44, 87, 67, 248, 15, 94, 114, 162, 65, 102, 198, 196, 38 }, null, true, null, null, new DateTime(2023, 12, 10, 11, 25, 16, 253, DateTimeKind.Local).AddTicks(4246) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("1e60f3c2-ada8-44fb-8819-ec052142f18d"), true, true, true, "Categories", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("36bbfaa0-64b0-4a2d-8c99-de5489c29662"), true, true, true, "Orders", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("50198ce8-ea83-4654-ba2c-0325592aa2d7"), true, true, true, "Products", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("602a8cd4-fa37-4393-bc6a-9e8f435c3820"), true, true, true, "Users", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("7e818d0b-d5d3-4b91-a851-cb8c3db42299"), true, true, true, "UserPermissions", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("839e4897-fcd2-4dd9-92f4-eabc499b947f"), true, true, true, "OrderProducts", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("8f2c7e28-5335-4415-806b-3ac9e5a59a23"), true, true, true, "Customers", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") },
                    { new Guid("dad5e5fb-d4ae-4201-b29f-f72f526edf59"), true, true, true, "UserRoles", true, new Guid("b813f428-7e93-44d1-b764-9a944da84a24") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PhoneNumber", "Status", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("153e6bbc-02f3-4f99-af6c-d20f55f0a42f"), null, null, "dntdat09@gmail.com", "Dat", "Thien", new byte[] { 226, 72, 179, 145, 98, 149, 60, 66, 45, 12, 77, 172, 146, 16, 58, 158, 41, 248, 48, 138, 143, 111, 156, 143, 192, 180, 67, 138, 178, 30, 15, 73, 67, 125, 152, 110, 6, 203, 66, 236, 204, 189, 66, 8, 225, 26, 96, 176, 151, 170, 65, 3, 236, 200, 178, 37, 202, 154, 248, 3, 157, 49, 183, 91 }, null, new byte[] { 76, 216, 143, 83, 15, 215, 15, 169, 152, 253, 160, 130, 194, 43, 107, 12, 56, 204, 100, 76, 90, 226, 76, 11, 16, 183, 158, 64, 78, 184, 57, 114, 227, 54, 244, 78, 122, 165, 70, 99, 252, 185, 130, 179, 183, 41, 84, 88, 197, 122, 203, 138, 244, 177, 204, 188, 63, 148, 169, 247, 200, 136, 103, 7, 92, 77, 245, 218, 68, 236, 246, 211, 195, 131, 213, 92, 237, 4, 202, 125, 212, 63, 213, 137, 116, 156, 138, 41, 79, 35, 70, 116, 106, 119, 216, 168, 198, 8, 217, 15, 107, 229, 22, 104, 253, 84, 88, 181, 77, 69, 45, 109, 90, 36, 172, 130, 235, 99, 51, 91, 91, 174, 174, 233, 10, 25, 53, 43 }, null, true, null, new Guid("b813f428-7e93-44d1-b764-9a944da84a24"), null, new DateTime(2023, 12, 10, 11, 25, 16, 253, DateTimeKind.Local).AddTicks(3916) });

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
