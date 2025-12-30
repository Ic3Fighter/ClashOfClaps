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
    public IActionResult Volumes(bool showMenu = true, params string[] team)
    {
        ViewData["menu"] = showMenu;
        var teams = team.Length < 1
            ? _options.Teams
            : _options.Teams.Where(x => team.Contains(x.Id));
        var volumes = _cacheBusinessProvider.GetVolumes();

        var model = teams.Select(x => new TeamViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Image = x.Image,
            Color = x.Color,
            Volume = volumes.GetValueOrDefault(x.Id, 0),
            IsActive = _cacheBusinessProvider.IsActive(x.Id),
        }).ToList();
        return View(model);
    }

    public IActionResult Points(bool showMenu = true)
    {
        ViewData["menu"] = showMenu;
        return View(_options.Teams.Select(x => new TeamViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Image = x.Image,
            Points = _cacheBusinessProvider.GetPoints().GetValueOrDefault(x.Id, x.StartingPoints),
        }));
    }
}
