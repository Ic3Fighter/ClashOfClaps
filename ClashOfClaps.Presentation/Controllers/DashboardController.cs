using Microsoft.AspNetCore.Mvc;

namespace ClashOfClaps.Presentation.Controllers;

public class DashboardController : Controller
{
    public IActionResult Volume(bool showMenu = true)
    {
        ViewData["menu"] = showMenu;
        return View();
    }

    public IActionResult Points(bool showMenu = true)
    {
        ViewData["menu"] = showMenu;
        return View();
    }
}
