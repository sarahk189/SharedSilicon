using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;
using System.Text;


namespace SharedSilicon.Controllers;

public class ContactController : Controller
{

	public IActionResult Index()
	{
		ViewData["Title"] = "Contact us";
		var viewModel = new ContactViewModel();

		if (TempData.ContainsKey("MessageSent"))
		{
			
			viewModel.Response = TempData["MessageSent"]!.ToString();
        }
		return View(viewModel);
	}

	

	[HttpPost]
	public async Task<IActionResult> Send(ContactViewModel model)
	{
		if (ModelState.IsValid)
		{
			var Dto = new ContactDto
			{
                FullName = model.FullName,
                Email = model.EmailAddress,
                Message = model.Message
            };
			using var client = new HttpClient();
			var json = JsonConvert.SerializeObject(Dto);
			using var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("https://localhost:7152/api/contact/send/?key=Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh", content);

			if (response.IsSuccessStatusCode)
			{
               TempData["MessageSent"] = "Your message has been sent!";
            }
            else
			{
                TempData["MessageSent"] = "An error occurred while sending your message. Please try again.";
            }
		}
		return RedirectToAction("Index");
      
    }

}


    