using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribeController(DataContext context) : ControllerBase
{

    #region CREATE

    [HttpPost]
    //[UseApiKey]
    public async Task<IActionResult> Create(SubscriberDto input)
    {
        if (ModelState.IsValid)
        {
            if (!await context.Subscribe.AnyAsync(x => x.Email == input.Email))

            {
                try
                {
                    var subscribeEntity = new SubscribeEntity
                    {
                        Email = input.Email,
                        Newsletter = input.Newsletter,
                        AdvertisingUpdates = input.AdvertisingUpdates,
                        WeekInReview = input.WeekInReview,
                        EventUpdates = input.EventUpdates,
                        StartupsWeekly = input.StartupsWeekly,
                        Subscribed = DateTime.Now
                    };

                    context.Subscribe.Add(subscribeEntity);
                    await context.SaveChangesAsync();

                    return Created("", null);
                }
                catch (DbUpdateException ex) 
                {
                    
                    Console.WriteLine(ex.InnerException?.Message);

                    return Problem("Unable to create subscription.");
                }
            }

            return Conflict("Your email address is already subscribed.");
        }

        return BadRequest("You must enter an valid email address.");

    }
    #endregion

    #region READ

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await context.Subscribe.ToListAsync();
        if (subscribers.Count != 0)
        {
            return Ok(subscribers);
        }

        return NotFound();

    }
    
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {

        var subscriber = await context.Subscribe.FirstOrDefaultAsync(x => x.Id == id);
        if(subscriber != null)
        {
            return Ok(subscriber);
        }

        return NotFound();
        

    }
    #endregion

    #region UPDATE

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, string email)
    {
        var subscriber = await context.Subscribe.FirstOrDefaultAsync(x => x.Id == id);
        if (subscriber != null)
        {

            subscriber.Email = email;
            context.Subscribe.Update(subscriber);
            await context.SaveChangesAsync();

            return Ok(subscriber);
        }

        return NotFound();

    }
    #endregion

    #region DELETE
    //[UseApiKey]
    [HttpDelete("{id}")]
    public async Task <IActionResult> DeleteOne(int id)
    {
        var subscriber = await context.Subscribe.FirstOrDefaultAsync(x => x.Id == id);
        if (subscriber != null)
        {
            context.Subscribe.Remove(subscriber);
            await context.SaveChangesAsync();

            return Ok();
        }
        return NotFound();

    }
    #endregion
}
