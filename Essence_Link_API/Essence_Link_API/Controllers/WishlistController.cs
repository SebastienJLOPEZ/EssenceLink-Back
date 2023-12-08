//using Microsoft.AspNetCore.Http;
using Essence_Link_API.Models;
using Essence_Link_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Essence_Link_API.Controllers;

[EnableCors("BaseAccess")]
[ApiController]
[Route("v1/api/[controller]")]

public class WishlistController : Controller
{
    private readonly WishlistService _WishlistService;

    public WishlistController(WishlistService WishlistService) =>
        _WishlistService = WishlistService;

    [HttpGet]
    [Authorize]
    public async Task<List<Wishlist>> Get() =>
        await _WishlistService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [Authorize]
    public async Task<List<Wishlist>> Get(string id) =>
        await _WishlistService.GetAsync(id);


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(Wishlist newWishlist)
    {
        await _WishlistService.CreateAsync(newWishlist);

        return CreatedAtAction(nameof(Get), new { id = newWishlist.Id }, newWishlist);
    }

    [HttpDelete("{id;length(24)}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var Wishlist = await _WishlistService.GetAsync(id);
        if (Wishlist is null)
        {
            return NotFound();
        }

        await _WishlistService.RemoveAsync(id);

        return NoContent();
    }
}
