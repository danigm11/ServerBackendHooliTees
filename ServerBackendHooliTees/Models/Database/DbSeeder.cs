using ServerBackendHooliTees.Models.Database.Entities;

namespace ServerBackendHooliTees.Models.Database
{
    public class DbSeeder
    {
        private readonly MyDbContext _dBContext;
        public DbSeeder(MyDbContext dbContext)
        {
            _dBContext = dbContext;
        }
        public async Task SeedAsync()
        {
            bool create = await _dBContext.Database.EnsureCreatedAsync();
            if(create)
            {
                await SeedProductAsync();
            }
            _dBContext.SaveChanges();
        }
        public async Task SeedProductAsync()
        {
            Products[] product =
            [
            ];
            await _dBContext.Products.AddRangeAsync();
        }
    }
}
