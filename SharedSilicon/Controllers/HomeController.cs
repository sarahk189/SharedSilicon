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
                _http.DefaultRequestHeaders.Add("ApiKey", "Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh");
                var response = await _http.PostAsync("https://localhost:7152/api/Subscribe?key=Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh", content);

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
                    ViewData["Status"] = "Unauthorized";
                }
                else
                {
                    ViewData["Status"] = "Error";
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

    [HttpPost]
    [Route("/unsubscribe")]
    public async Task<IActionResult> Unsubscribe(int id)
    {
        try
        {
            _http.DefaultRequestHeaders.Add("ApiKey", "Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh");
            var response = await _http.DeleteAsync($"https://localhost:7152/api/Subscribe/{id}?key=Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh");

            if (response.IsSuccessStatusCode)
            {
                ViewData["Status"] = "Success";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ViewData["Status"] = "NotFound";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ViewData["Status"] = "Unauthorized";
            }
            else
            {
                ViewData["Status"] = "Error";
            }
        }
        catch
        {
            ViewData["Status"] = "ConnectionFailed";
        }

        return View("Index");
    }
}


    //public IActionResult Error404()
    //{
    //    return View("Error404/Index");
    //}


