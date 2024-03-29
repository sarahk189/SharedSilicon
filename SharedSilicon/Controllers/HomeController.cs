using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedSilicon.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Unicode;


namespace SharedSilicon.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        var viewModel = new SubscribeViewModel();
        return View();
    }


    [HttpPost]
    public async Task <IActionResult> Subscribe(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            //    try
            //    {
            //        var content = new StringContent/JsonConverter.SerializeObject(viewModel), Encoding.Utf8, "application/json");
            //        var response = await _http.PostAsync("https:...", content);

            //        if (response.ISSuccessStatusCode)
            //        {
            //            ViewData["Status"] = "Success";
            //        }
            //        else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            //        {
            //            ViewData["Status"] = "AlreadyExists";
            //        }
            //        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //        {
            //            ViewData["Status"] = "Unauhorized";
            //        }
            //    }
            //    catch
            //    {
            //        ViewData["Status"] = "ConnectionFailed";
            //    }
            //}
            //else
            //{
            //    ViewData["Status"] = "Invalid";
        }
        return View(viewModel);
    }

}
