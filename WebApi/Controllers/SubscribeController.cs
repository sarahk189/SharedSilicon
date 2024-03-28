using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController(DataContext context) : ControllerBase
    {

        private readonly DataContext _context = context;

        #region CREATE

        [HttpPost]
        public async Task<IActionResult> Create(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (!await _context.Subscribe.AnyAsync(x => x.Email == email))

                {
                    try
                    {
                        var subscribeEntity = new SubscribeEntity
                        {
                            Email = email,
                            Subscribed = DateTime.Now
                        };

                        _context.Subscribe.Add(subscribeEntity);
                        await _context.SaveChangesAsync();

                        return Created("", null);
                    }
                    catch 
                    { 
                        return Problem("Unable to create subscription.");
                    }
                }

                return Conflict("Your email address is already subscribed.");
            }

            return BadRequest();

        }
        #endregion

        #region READ

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();

        }
        
        

        [HttpGet]
        public IActionResult GetOne()
        {
            return Ok();

        }
        #endregion

        #region UPDATE

        [HttpPut]
        public IActionResult Update()
        {
            return Ok();

        }
        #endregion

        #region DELETE

        [HttpPost]
        public IActionResult Delete()
        {
            return Ok();

        }
        #endregion
    }
}
