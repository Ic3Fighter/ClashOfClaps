using ClashOfClaps.Data.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Presentation.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationOptions _options;

    public DashboardController(IOptionsSnapshot<ApplicationOptions> options)
    {
        _options = options.Value;
    }

    [HttpGet]
    public IActionResult Volumes(bool showMenu = true)
    {
        ViewData["menu"] = showMenu;
        return View(_options.Teams);
    }

    public IActionResult Points(bool showMenu = true)
    {
        ViewData["menu"] = showMenu;
        return View(_options.Teams);
    }
}
