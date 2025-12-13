using ClashOfClaps.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClaps.Presentation.Controllers;

public class ApiController : Controller
{
    public IActionResult Volume()
    {
        var rnd = new Random();
        var data = Enumerable.Range(0, 3)
            .Select(i => new ApplauseVolume
            {
                TeamName = $"Team{i}",
                Volume = rnd.NextDouble() * 100
            })
            .ToDictionary(x => x.TeamName, y => y);
        return Json(data);
    }
}