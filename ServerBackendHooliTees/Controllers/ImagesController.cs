using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Models.Dtos;
using ServerBackendHooliTees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ServerBackendHooliTees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private const string IMAGES_PATH = "images";

        private readonly MyDbContext _dbContext;

        public ImagesController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<ImageDto> Get()
        {
            return _dbContext.Images.Select(ToDto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateImageDto imageDto)
        {
            using Stream stream = imageDto.File.OpenReadStream();
            string imagePath = $"{Guid.NewGuid()}_{imageDto.File.FileName}";
            string imageUrl = await Services.FileService.SaveAsync(stream, IMAGES_PATH, imagePath);

            Image newImage = new Image()
            {
                Name = imageDto.Name,
                URL = imageUrl
            };

            await _dbContext.Images.AddAsync(newImage);
            await _dbContext.SaveChangesAsync();

            ImageDto imageCreated = ToDto(newImage);

            return Created($"/{newImage.Id}", imageCreated);
        }

        private ImageDto ToDto(Image image)
        {
            return new ImageDto()
            {
                Name = image.Name,
                URL = image.URL,
            };
        }
    }
}
