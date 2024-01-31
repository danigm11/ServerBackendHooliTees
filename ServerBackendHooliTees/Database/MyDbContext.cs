using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Database.Entities;

namespace ServerBackendHooliTees.Database;

public class MyDbContext : DbContext
{

    private const string DATBASE_PATH = "hoolitees.db";

    //  Tablas
    public DbSet<Users> Users { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }

    // Configurar EF para crear un archivo de base de datos Sqlite
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        options.UseSqlite($"DataSource={baseDir}{DATBASE_PATH}");
    }

}
