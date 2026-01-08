using ClashOfClaps.Business.BusinessProviders;
using ClashOfClaps.Data.Options;
using ClashOfClaps.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Presentation.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationOptions _options;
    private readonly CacheBusinessProvider _cacheBusinessProvider;

    public DashboardController(IOptionsSnapshot<ApplicationOptions> options,
        CacheBusinessProvider cacheBusinessProvider)
    {
        _options = options.Value;
        _cacheBusinessProvider = cacheBusinessProvider;
    }

    [HttpGet]
    public IActionResult Volumes(bool showMenu = true, bool dark = false, params string[] team)
    {
        ViewData["menu"] = showMenu;
        ViewData["dark"] = dark;
        var volumes = _cacheBusinessProvider.GetVolumes();
        var isAnyActive = _cacheBusinessProvider.IsAnyTeamActive();

        var model = _options.Teams.Select(x => new TeamViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Image = x.Image,
            Color = x.Color,
            Volume = volumes.GetValueOrDefault(x.Id, 0),
            IsActive = !isAnyActive || _cacheBusinessProvider.IsActive(x.Id),
            IsSelected = team.Length < 1 || team.Contains(x.Id),
        }).ToList();
        return View(model);
    }

    public IActionResult Points(bool showMenu = true, bool dark = false)
    {
        ViewData["menu"] = showMenu;
        ViewData["dark"] = dark;
        return View(_options.Teams.Select(x => new TeamViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Image = x.Image,
            Color = x.Color,
            Points = _cacheBusinessProvider.GetPoints().GetValueOrDefault(x.Id, x.StartingPoints),
        }));
    }
}
