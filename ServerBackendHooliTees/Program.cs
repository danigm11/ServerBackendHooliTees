using Microsoft.IdentityModel.Tokens;
using ServerBackendHooliTees.Models.Database;
using System.Text;

namespace ServerBackendHooliTees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configuramos para que el directorio de trabajo sea donde está el ejecutable
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<MyDbContext>();

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                string Key = Environment.GetEnvironmentVariable("JWT_KEY");

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //  Validar el emisor del token.
                    ValidateIssuer = false,

                    //  Audiencia
                    ValidateAudience = false,

                    //  Idicamos la clave
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
                };
            });

            var app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                MyDbContext dbContext = scope.ServiceProvider.GetService<MyDbContext>();
                dbContext.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                //Uso de CORS
                app.UseCors(config => config
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());

            }

            app.UseHttpsRedirection();

            // JWT
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            
        app.Run();
        }
    }
}
