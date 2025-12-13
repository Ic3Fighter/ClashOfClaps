using ClashOfClaps.Business.BusinessProviders;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClaps.Presentation.Controllers;

public class ApiController : Controller
{
    private readonly CacheBusinessProvider _cacheBusinessProvider;

    public ApiController(CacheBusinessProvider cacheBusinessProvider)
    {
        _cacheBusinessProvider = cacheBusinessProvider;
    }

    public IActionResult Volume()
    {
        _cacheBusinessProvider.RandomizeVolumes();
        return Json(_cacheBusinessProvider.GetVolumes());
    }
}