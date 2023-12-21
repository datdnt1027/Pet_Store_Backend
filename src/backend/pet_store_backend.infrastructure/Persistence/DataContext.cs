using System.Reflection;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<UserPermission> UserPermissions { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<pet_store_backend.domain.Entities.Orders.Order> Orders { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        SeedAdminData(modelBuilder);
        SeedGuestData(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac
            .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private static List<string> GetTableNamesFromTableKey()
    {
        var tableKeyType = typeof(TableKey);
        var fields = tableKeyType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        var tables = new List<string>();

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(string) && !field.Name.EndsWith("Id"))
            {
                if (field.GetValue(null) is string value)
                {
                    tables.Add(value);
                }
            }
        }
        return tables;
    }

    private static void SeedAdminData(ModelBuilder modelBuilder)
    {
        CreatePasswordHash("admin123$@", out byte[] passwordHash, out byte[] passwordSalt);

        var user = User.Create(
           firstName: "Dat",
           lastName: "Thien",
           email: "dntdat09@gmail.com",
           passwordHash: passwordHash,
           passwordSalt: passwordSalt,
           null!);

        user.UpdateVerifiedAt(DateTime.Now);

        var userRole = UserRole.Create(UserRoleKey.AdminRoleName, null!, null!);

        var tables = GetTableNamesFromTableKey();
        var userPermissions = tables.Select(table =>
            UserPermission.CreatePermission(
                table,
                true,
                true,
                true,
                true,
                userRole.Id,
                null!
            )
        ).ToList();

        modelBuilder.Entity<UserRole>().HasData(userRole);
        modelBuilder.Entity<UserPermission>().HasData(userPermissions);

        user.UpdateUserRoleId(userRole.Id);
        modelBuilder.Entity<User>().HasData(user);
    }


    private static void SeedGuestData(ModelBuilder modelBuilder)
    {
        CreatePasswordHash("Test123$@", out byte[] passwordHash, out byte[] passwordSalt);

        var customer = Customer.Create(
           firstName: "Dat",
           lastName: "Thien",
           email: "20110629@student.hcmute.edu.vn",
           passwordHash: passwordHash,
           passwordSalt: passwordSalt,
           null!);

        customer.UpdateVerifiedAt(DateTime.Now);

        var userRole = UserRole.Create(UserRoleKey.UserRoleName, null!, null!);
        modelBuilder.Entity<UserRole>().HasData(userRole);

        customer.UpdateUserRoleId(userRole.Id);
        modelBuilder.Entity<Customer>().HasData(customer);

    }
}