using Microsoft.AspNetCore.Mvc;

namespace ClashOfClaps.Presentation.Controllers;

public class DashboardController : Controller
{
    public IActionResult Volume() => View();
}
