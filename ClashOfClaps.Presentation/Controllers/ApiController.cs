using ClashOfClaps.Business.BusinessProviders;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClaps.Presentation.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private readonly CacheBusinessProvider _cacheBusinessProvider;
    private readonly PointsBusinessProvider _pointsBusinessProvider;

    public ApiController(CacheBusinessProvider cacheBusinessProvider,
        PointsBusinessProvider pointsBusinessProvider)
    {
        _cacheBusinessProvider = cacheBusinessProvider;
        _pointsBusinessProvider = pointsBusinessProvider;
    }

    [HttpGet]
    [Route("volumes")]
    public IActionResult Volumes()
    {
        _cacheBusinessProvider.RandomizeVolumes();
        return Ok(_cacheBusinessProvider.GetVolumes());
    }

    [HttpGet]
    [Route("points")]
    public IActionResult Points() => Ok(_cacheBusinessProvider.GetPoints());

    [HttpPut]
    [Route("points/{team}")]
    public IActionResult Set(string team, int points)
    {
        if (_pointsBusinessProvider.Set(team, points)) return Ok();
        return BadRequest();
    }

    [HttpPut]
    [Route("points/{team}/increase")]
    public IActionResult Increase(string team, int points = 1)
    {
        if (_pointsBusinessProvider.Change(team, points)) return Ok();
        return BadRequest();
    }

    [HttpPut]
    [Route("points/{team}/decrease")]
    public IActionResult Decrease(string team, int points = 1)
    {
        if (_pointsBusinessProvider.Change(team, -1 * points)) return Ok();
        return BadRequest();
    }
}
