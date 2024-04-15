using Infrastructure.Contexts;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
