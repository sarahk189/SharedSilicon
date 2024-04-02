using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;


namespace SharedSilicon.Controllers;

public class HomeController(HttpClient http) : Controller
{
    private readonly HttpClient _http = http;

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        var viewModel = new SubscribeViewModel();
        return View(viewModel);
    }


    [HttpPost]
    [Route("/")]
    public async Task<IActionResult> Subscribe(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel.Form), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("https://localhost:7152/api/Subscribe", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "AlreadyExists";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewData["Status"] = "Unauhorized";
                }
            }
            catch
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        else
        {
            ViewData["Status"] = "Invalid";
        }
        return View("Index", viewModel);
    }


}
