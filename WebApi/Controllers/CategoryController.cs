using Infrastructure.Contexts;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(DataContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await context.Categories.OrderBy(x => x.Name).ToListAsync();
           
            return Ok(CategoryFactory.Create(categories));
        }
	}
}
