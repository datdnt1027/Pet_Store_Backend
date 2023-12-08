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
                    { new Guid("03dac767-c4d0-422d-a558-16d604a15bf9"), true, "Admin" },
                    { new Guid("77d315f9-5dd8-43a9-be03-2c9c9187c6ea"), true, "User" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerRoleId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "TokenExpires", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("a65bda30-abb0-493e-906a-4d2887dc816c"), new Guid("77d315f9-5dd8-43a9-be03-2c9c9187c6ea"), "20110629@student.hcmute.edu.vn", "Dat", "Thien", new byte[] { 65, 18, 130, 3, 48, 127, 240, 75, 204, 38, 193, 199, 193, 30, 233, 199, 178, 48, 77, 92, 94, 54, 16, 53, 59, 205, 86, 121, 131, 125, 225, 146, 179, 171, 56, 104, 211, 241, 193, 38, 76, 67, 175, 9, 152, 214, 156, 94, 133, 23, 231, 107, 41, 28, 227, 169, 240, 212, 146, 170, 64, 28, 213, 250 }, null, new byte[] { 131, 171, 100, 194, 26, 52, 31, 109, 118, 51, 46, 64, 147, 189, 234, 215, 53, 126, 251, 180, 144, 67, 223, 208, 127, 170, 134, 226, 211, 197, 46, 158, 114, 41, 3, 162, 8, 9, 194, 130, 130, 190, 160, 3, 17, 201, 42, 167, 131, 253, 211, 114, 148, 45, 244, 55, 251, 170, 39, 228, 233, 32, 23, 232, 139, 240, 67, 23, 247, 15, 211, 242, 164, 0, 154, 215, 155, 29, 86, 183, 254, 52, 48, 60, 34, 248, 254, 174, 225, 228, 168, 214, 40, 236, 91, 199, 226, 31, 181, 174, 186, 109, 190, 226, 7, 235, 247, 21, 126, 153, 125, 224, 199, 239, 71, 66, 239, 170, 33, 62, 224, 249, 204, 107, 30, 195, 213, 194 }, null, null, new DateTime(2023, 12, 7, 16, 50, 2, 436, DateTimeKind.Local).AddTicks(3008) });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("1b986893-6686-4a99-b629-963456801662"), true, true, true, "Customers", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("43d49d3d-6d17-4399-a690-1271a3ef553f"), true, true, true, "Categories", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("476c48dd-4477-40c4-84d2-1324efe179e6"), true, true, true, "Products", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("55298d74-9bdd-474b-abbf-0f69c355c47d"), true, true, true, "Orders", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("6ae0c3f2-3d68-4152-9e83-c988d7fb1132"), true, true, true, "OrderProducts", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("8853350f-9cc5-4295-9d30-5f4ef1c94177"), true, true, true, "Users", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("9164c873-9639-44f2-9ab0-35dd17f9cbe8"), true, true, true, "UserRoles", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") },
                    { new Guid("e988548f-6302-4693-8a79-c1c816042a5e"), true, true, true, "UserPermissions", true, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("a1b3d67c-3fa8-4332-89bf-6552049d5882"), "dntdat09@gmail.com", "Dat", "Thien", new byte[] { 4, 117, 104, 153, 6, 41, 175, 54, 72, 252, 99, 105, 69, 134, 138, 68, 23, 0, 103, 23, 55, 157, 15, 63, 81, 30, 217, 110, 110, 98, 97, 109, 136, 173, 42, 152, 41, 179, 8, 205, 11, 27, 115, 212, 12, 64, 242, 126, 109, 248, 78, 188, 58, 8, 170, 50, 146, 239, 214, 127, 251, 186, 46, 98 }, null, new byte[] { 182, 194, 165, 151, 73, 213, 197, 185, 243, 44, 178, 78, 251, 243, 108, 135, 116, 100, 127, 186, 44, 72, 93, 109, 60, 4, 133, 222, 171, 226, 99, 42, 143, 219, 20, 103, 164, 111, 52, 85, 151, 69, 130, 173, 135, 101, 160, 224, 251, 159, 155, 11, 222, 54, 213, 165, 24, 136, 116, 89, 222, 251, 132, 78, 227, 52, 194, 162, 137, 106, 156, 201, 185, 115, 191, 98, 125, 41, 206, 220, 187, 56, 55, 201, 137, 255, 166, 217, 73, 165, 75, 0, 106, 167, 248, 17, 54, 233, 42, 47, 231, 198, 191, 51, 184, 139, 195, 190, 28, 54, 255, 241, 19, 230, 39, 212, 6, 91, 249, 17, 16, 248, 121, 112, 210, 148, 253, 238 }, null, new Guid("03dac767-c4d0-422d-a558-16d604a15bf9"), null, new DateTime(2023, 12, 7, 16, 50, 2, 436, DateTimeKind.Local).AddTicks(2734) });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerRoleId",
                table: "Customers",
                column: "CustomerRoleId",
                unique: true,
                filter: "[CustomerRoleId] IS NOT NULL");

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
                column: "UserRoleId",
                unique: true,
                filter: "[UserRoleId] IS NOT NULL");

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
