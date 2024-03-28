using Microsoft.AspNetCore.Mvc;

namespace SharedSilicon.Controllers;

public class SiteSettings : Controller
{
    public IActionResult ChangeTheme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(30),
        };
        Response.Cookies.Append("ThemeMode", mode, option);
        return Ok();
    }
}
