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
                name: "UserPermissions",
                columns: table => new
                {
                    UserPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "Status", "UserRoleName" },
                values: new object[,]
                {
                    { new Guid("14227c76-a4f0-434d-bdbf-0f72b38a36b4"), true, "User" },
                    { new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0"), true, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "Create", "Deactive", "Read", "TableName", "Update", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("3d5f15b8-ced3-4d87-b6d4-b61331ec1458"), true, true, true, "Users", true, new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0") },
                    { new Guid("5d58ce28-eff0-488e-9b4b-c92ce7e899bf"), true, true, true, "Categories", true, new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0") },
                    { new Guid("6f04a512-bf01-4870-87f8-b3a6b877ffa9"), true, true, true, "Products", true, new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0") },
                    { new Guid("7228ee6a-cdec-465e-9410-1ee481c503d2"), true, true, true, "UserPermissions", true, new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0") },
                    { new Guid("84fcf437-b1e2-49ce-ba1b-acd9dda98846"), true, false, false, "Products", false, new Guid("14227c76-a4f0-434d-bdbf-0f72b38a36b4") },
                    { new Guid("b4197904-856d-40af-a71d-cebe00e2c960"), true, true, true, "UserRoles", true, new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "TokenExpires", "UserRoleId", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("2f68514a-9fc6-4df4-98bb-b957149bba83"), "dntdat09@gmail.com", "Dat", "Thien", new byte[] { 118, 72, 136, 234, 190, 194, 76, 66, 162, 237, 64, 32, 8, 245, 255, 132, 52, 211, 19, 5, 44, 82, 120, 61, 116, 45, 213, 254, 126, 233, 12, 248, 252, 105, 4, 190, 198, 188, 230, 140, 214, 40, 192, 223, 105, 249, 62, 88, 161, 108, 68, 76, 253, 179, 183, 117, 172, 12, 150, 218, 20, 188, 242, 5 }, null, new byte[] { 200, 131, 170, 132, 209, 192, 174, 149, 107, 207, 69, 112, 92, 1, 23, 44, 137, 197, 148, 211, 211, 45, 84, 169, 132, 254, 157, 0, 209, 221, 138, 49, 218, 193, 54, 36, 32, 151, 208, 156, 52, 189, 184, 180, 110, 134, 91, 72, 149, 160, 248, 32, 11, 217, 201, 195, 139, 217, 197, 114, 114, 128, 157, 67, 24, 46, 26, 246, 189, 4, 228, 2, 161, 131, 85, 50, 209, 188, 161, 34, 248, 48, 161, 32, 81, 153, 245, 104, 63, 37, 215, 64, 77, 188, 10, 5, 105, 85, 103, 159, 62, 153, 0, 42, 48, 242, 78, 208, 241, 249, 70, 133, 128, 151, 12, 197, 81, 143, 220, 41, 30, 165, 20, 125, 198, 234, 162, 203 }, null, new Guid("fb5f8473-9c90-48ee-a5d9-7ff3d4e3b4a0"), null, new DateTime(2023, 11, 25, 12, 12, 29, 687, DateTimeKind.Local).AddTicks(71) });

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
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
