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
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: true)
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
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: true)
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
                    { new Guid("3589728c-dd7b-4be0-84fb-ccdfd0bded91"), true, "User" },
                    { new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1"), true, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerRoleId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("1114c365-9190-4410-a40b-da23fece902c"), new Guid("3589728c-dd7b-4be0-84fb-ccdfd0bded91"), "20110629@student.hcmute.edu.vn", "Dat", "Thien", new byte[] { 174, 85, 1, 1, 138, 112, 174, 143, 142, 199, 9, 197, 28, 99, 164, 199, 160, 236, 19, 56, 144, 199, 37, 255, 190, 237, 182, 42, 83, 72, 193, 24, 249, 14, 23, 250, 50, 87, 252, 223, 71, 85, 31, 163, 225, 236, 60, 211, 96, 196, 96, 66, 80, 168, 7, 66, 88, 123, 102, 34, 160, 246, 205, 251 }, null, new byte[] { 204, 136, 38, 22, 19, 174, 62, 94, 204, 237, 48, 76, 19, 87, 251, 184, 124, 72, 184, 239, 86, 193, 106, 47, 3, 122, 199, 43, 49, 232, 202, 193, 231, 204, 145, 178, 26, 246, 49, 206, 242, 17, 104, 89, 41, 209, 52, 231, 223, 81, 165, 66, 140, 214, 112, 55, 73, 127, 125, 83, 26, 204, 210, 12, 7, 156, 65, 202, 191, 76, 43, 20, 67, 125, 133, 6, 193, 196, 103, 102, 236, 201, 234, 69, 13, 233, 13, 209, 39, 115, 160, 1, 19, 155, 39, 246, 114, 255, 77, 7, 211, 126, 105, 81, 73, 24, 243, 40, 226, 246, 135, 17, 226, 218, 102, 67, 29, 54, 114, 121, 127, 34, 230, 178, 45, 217, 116, 34 }, null, null, new DateTime(2023, 12, 8, 17, 59, 18, 556, DateTimeKind.Local).AddTicks(7620) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("25cd3384-623d-402c-8b97-c834aaa46353"), true, true, true, "UserPermissions", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("461ab9c6-0d0c-4c99-bda7-e413684dc710"), true, true, true, "OrderProducts", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("67809df8-a274-4622-bd49-fcc32af2381b"), true, true, true, "Products", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("7c1afd3f-48ff-4c81-a7c4-1b54657c28d4"), true, true, true, "UserRoles", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("9f7c20c8-c2d5-4e0b-a43c-d5d16aba828c"), true, true, true, "Users", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("a91c5a02-bc84-4fb0-93aa-a56bb81301df"), true, true, true, "Customers", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("cb7f1cbf-850d-47b8-aa5e-42e6163dd1e5"), true, true, true, "Categories", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") },
                    { new Guid("f29ec49a-b9c3-4909-976b-5d53921b4240"), true, true, true, "Orders", true, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("533033ee-c0c2-4ccc-b4cb-edffa7f58493"), "dntdat09@gmail.com", "Dat", "Thien", new byte[] { 27, 31, 0, 208, 55, 158, 79, 90, 187, 80, 223, 238, 147, 80, 217, 35, 155, 115, 79, 82, 107, 195, 172, 137, 255, 142, 193, 174, 141, 234, 66, 193, 192, 69, 181, 63, 63, 110, 107, 107, 227, 154, 115, 45, 170, 81, 175, 158, 30, 187, 141, 133, 189, 48, 154, 39, 43, 30, 93, 222, 139, 185, 199, 250 }, null, new byte[] { 84, 212, 36, 244, 252, 107, 179, 77, 25, 170, 156, 242, 190, 217, 242, 225, 120, 128, 171, 219, 39, 233, 46, 147, 157, 121, 15, 190, 50, 211, 213, 78, 68, 47, 26, 126, 185, 12, 102, 48, 200, 138, 67, 189, 200, 196, 45, 140, 165, 72, 98, 79, 196, 53, 81, 3, 36, 179, 85, 189, 45, 243, 241, 148, 112, 41, 79, 189, 206, 75, 167, 255, 153, 195, 135, 157, 148, 93, 14, 71, 18, 229, 78, 236, 37, 122, 243, 160, 111, 17, 172, 163, 175, 21, 233, 31, 25, 155, 83, 48, 115, 253, 27, 199, 179, 143, 35, 176, 73, 65, 225, 27, 103, 189, 56, 213, 55, 221, 95, 54, 217, 163, 137, 162, 236, 31, 179, 224 }, null, new Guid("d7473b0b-9ab5-4715-864a-136a99d9cac1"), null, new DateTime(2023, 12, 8, 17, 59, 18, 556, DateTimeKind.Local).AddTicks(7165) });

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
