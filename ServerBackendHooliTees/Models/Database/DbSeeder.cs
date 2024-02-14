using ServerBackendHooliTees.Models.Database.Entities;

namespace ServerBackendHooliTees.Models.Database;

public class DbSeeder
{
    private readonly MyDbContext _dbContextHoolitees;

    public DbSeeder(MyDbContext dbContext)
    {
        _dbContextHoolitees = dbContext;
    }

    public async Task SeedAsync()
    {
        bool created = /*true;*/ await _dbContextHoolitees.Database.EnsureCreatedAsync();

        if (created)
        {
            await SeedProductsAsync();
        }

        _dbContextHoolitees.SaveChanges();
    }

    private async Task SeedProductsAsync()
    {
        Products[] products =
        [
            new Products()
            {
                Name = "Camiseta RM",
                Description = "Camiseta del Real Madrileño, equipo de la liga Española de futbol.\n Basicamente el mejor del mundo",
                Price = 15.99m,
                Stock = 10,
                Image = "images/1.jpg"
            },
            new Products()
            {
                Name = "Camiseta Barceloña",
                Description = "Camiseta del Barcelona, equipo de Europa League\n Basicamente imitación del levante",
                Price = 15.99m,
                Stock = 10,
                Image = "images/2.jpg"
            },
            new Products()
            {
                Name = "Camiseta Boqueron",
                Description = "Camiseta del Malaga, equipo con la mejor afición.\n Málaga La Bombonera\nFlor de la Costa del Sol\nTiene equipo de Primera\nEn el fútbol español\nVamos a La Rosaleda\nEs campo internacional\nLa afición jamás se queda\nCon nosotros no hay quien pueda\nEl Málaga va a jugar\nColores blanquiazules\nJugando con tesón\nPoner en el partido\nCoraje y corazón\nLuchando con firmeza\nSerás el triunfador\nPor eso con sus gritos\nTe anima la afición\nSin orgullo cuando gana\nCuando pierde sin rencor\nEntre la afición más sana\nEl Málaga es campeón\nAdelante jugadores\nPor el triunfo frente al gol\nDel deporte defensores\nMalagueños y señores\nEn el fútbol español\nColores blanquiazules\nJugando con tesón\nPoner en el partido\nCoraje y corazón\nLuchando con firmeza\nSerás el triunfador\nPor eso con sus gritos\nTe anima la afición\n¡¡ MÁ - LA - GA !!",
                //Description = "Málaga La Bombonera\\n\r\nFlor de la Costa del Sol\\n\r\nTiene equipo de Primera\\n\r\nEn el fútbol español\\n\r\nVamos a La Rosaleda\\n\r\nEs campo internacional\\n\r\nLa afición jamás se queda\\n\r\nCon nosotros no hay quien pueda\\n\r\nEl Málaga va a jugar\\n\\n\r\nColores blanquiazules\\n\r\nJugando con tesón\\n\r\nPoner en el partido\\n\r\nCoraje y corazón\\n\r\nLuchando con firmeza\\n\r\nSerás el triunfador\\n\r\nPor eso con sus gritos\\n\r\nTe anima la afición\\n\\n\r\nSin orgullo cuando gana\\n\r\nCuando pierde sin rencor\\n\r\nEntre la afición más sana\\n\r\nEl Málaga es campeón\\n\r\nAdelante jugadores\\n\r\nPor el triunfo frente al gol\\n\r\nDel deporte defensores\\n\r\nMalagueños y señores\\n\r\nEn el fútbol español\\n\\n\r\nColores blanquiazules\\n\r\nJugando con tesón\\n\r\nPoner en el partido\\n\r\nCoraje y corazón\\n\r\nLuchando con firmeza\\n\r\nSerás el triunfador\\n\r\nPor eso con sus gritos\\n\r\nTe anima la afición\\n\r\n¡¡ MÁ - LA - GA !!\\n\r\n",
                Price = 15.99m,
                Stock = 10,
                Image = "images/11.jpg"
            },
            new Products()
            {
                Name = "Camiseta Sociedad Real",
                Description = "Camiseta de la Real Sociedad.",
                Price = 15.99m,
                Stock = 10,
                Image = "images/6.jpg"
            },
            new Products()
            {
                Name = "Camiseta Betis BalonFoot",
                Description = "Camiseta del Betis. Mucho mejor que el sevilla.",
                Price = 15.99m,
                Stock = 10,
                Image = "images/4.jpg"
            },
            new Products()
            {
                Name = "Camiseta PSGuarros",
                Description = "Club de moros.",
                Price = 15.99m,
                Stock = 10,
                Image = "images/9.jpg"
            },
            new Products()
            {
                Name = "Camiseta Romana",
                Description = "Camiseta de la Roma. Todos los caminos llegan a ella.",
                Price = 15.99m,
                Stock = 10,
                Image = "images/10.jpg"
            },
            new Products()
            {
                Name = "Camiseta LVP",
                Description = "Camiseta del Liverpool. You never walk alone",
                Price = 15.99m,
                Stock = 10,
                Image = "images/8.jpg"
            },
            new Products()
            {
                Name = "Camiseta de Bilbao",
                Description = "Camiseta de Bilbao. Los del Norte",
                Price = 15.99m,
                Stock = 10,
                Image = "images/5.jpg"
            },
            new Products()
            {
                Name = "Camiseta City de Manchester",
                Description = "Camiseta del City. Solo entiende de Dinero",
                Price = 15.99m,
                Stock = 10,
                Image = "images/7.jpg"
            },
            new Products()
            {
                Name = "Camiseta de los Malavariks",
                Description = "Son el mejor equipo de la NBA",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b1.png"
            },
            new Products()
            {
                Name = "Camiseta de los Lagos",
                Description = "Un tal pablo,que es muy alto, juega aquí",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b2.png"
            },
            new Products()
            {
                Name = "Camiseta de los toros de chicago",
                Description = "Posiblemente el equipo favorito de Al Capone",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b3.png"
            },
            new Products()
            {
                Name = "Camiseta de los guerreros",
                Description = "La mejor camiseta sin duda\nHecho para guerreros de verdad",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b4.png"
            },
            new Products()
            {
                Name = "Camiseta de los ciervos",
                Description = "No se porque dejan ciervos jugar contra humanos, pero es su equipación",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b5.png"
            },
            new Products()
            {
                Name = "Camiseta del calor",
                Description = "Para los conchasumare del team calor",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b6.png"
            },
            new Products()
            {
                Name = "Camiseta de los bola",
                Description = "Con esta camiseta puedes tirar mejor que ese tal pablo",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b7.png"
            },
            new Products()
            {
                Name = "Camiseta de los soles",
                Description = "Para los fervientes seguidores que les gusta el día",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b8.png"
            },
            new Products()
            {
                Name = "Camiseta irlandesa",
                Description = "Para los aficionados al dia de san patricio",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b9.png"
            },
            new Products()
            {
                Name = "Camiseta de los pepitos",
                Description = "Sí eres pepito, aqui esta tu camiseta",
                Price = 19.99m,
                Stock = 10,
                Image = "images/b10.png"
            },
        ];

        await _dbContextHoolitees.Products.AddRangeAsync(products);
    }
}