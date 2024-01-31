using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Database.Entities;

namespace ServerBackendHooliTees.Database;

public class MyDbContext : DbContext
{

    private const string DATBASE_PATH = "hoolitees";

    //  Tablas
    public DbSet<Users> Users { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set;}

    // Configurar EF para crear un archivo de base de datos Sqlite
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        options.UseSqlite($"DataSource={baseDir}{DATBASE_PATH}");
    }

}
