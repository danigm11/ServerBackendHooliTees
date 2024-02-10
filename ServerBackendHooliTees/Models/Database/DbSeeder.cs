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
                await SeedImagesAsync();
            }
            _dBContext.SaveChanges();
        }
        public async Task SeedImagesAsync()
        {
            Image[] images =
            [

            ];
            await _dBContext.Images.AddRangeAsync(images);
        }
    }
}
