using Deli.DATA.DTOs;
using Deli.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deli.Controllers;

public class HomePageController : BaseController
{
    private readonly IHomepageServices _homepageServices;

    public HomePageController(IHomepageServices homepageServices)
    {
        _homepageServices = homepageServices;
    }
    [HttpGet("DiscoverDeli")]
    public async Task<ActionResult<DiscoverDeliDto>> GetDiscoverDeli() => Ok(await _homepageServices.GetDiscoverDeli(Language));
    [HttpPut("DiscoverDeli{id}")]
    public async Task<ActionResult> Update(Guid id, DiscoverDeliUpdate discoverDeliUpdate) => Ok(await _homepageServices.Update(id, discoverDeliUpdate, Language));
    [HttpGet("WhoAreWe")]
    public async Task<ActionResult<WhoAreWeDto>> GetMyWhoAreWe() => Ok(await _homepageServices.GetMyWhoAreWe(Language));
    [HttpPut("WhoAreWe{id}")]
    public async Task<ActionResult> Update(Guid id, WhoAreWeUpdate whoAreWeUpdate) => Ok(await _homepageServices.Update(id, whoAreWeUpdate, Language));
}