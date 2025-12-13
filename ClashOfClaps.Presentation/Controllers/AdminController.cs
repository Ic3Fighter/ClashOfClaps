using ClashOfClaps.Data.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Presentation.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationOptions _options;

    public AdminController(IOptionsSnapshot<ApplicationOptions> options)
    {
        _options = options.Value;
    }

    public IActionResult Index() => View(_options.Teams);
}