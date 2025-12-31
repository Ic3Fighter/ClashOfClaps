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

    #region Volumes

    [HttpGet]
    [Route("volumes")]
    public IActionResult Volumes()
    {
        _cacheBusinessProvider.MeasureVolumes();
        return Ok(_cacheBusinessProvider.GetVolumes().ToDictionary(x => x.Key, 
            y => new
            {
                Volume = y.Value, 
                IsActive = _cacheBusinessProvider.IsActive(y.Key)
            }));
    }

    [HttpPut]
    [Route("volumes")]
    public IActionResult ResetVolumes()
    {
        _cacheBusinessProvider.ResetVolumes();
        return Ok();
    }

    #endregion

    #region Points

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

    #endregion

    #region Team

    [HttpPost]
    [Route("team/{team}/active/{isActive?}")]
    public IActionResult SetActive(string team, bool? isActive)
    {
        _cacheBusinessProvider.SetActive(team, isActive ?? true);
        return Ok();
    }

    #endregion
}
