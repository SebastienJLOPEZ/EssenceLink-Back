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
public class ReviewController : Controller
{
    private readonly ReviewService _ReviewService;

    public ReviewController(ReviewService ReviewService) =>
        _ReviewService = ReviewService;

    [HttpGet]
    [Authorize]
    public async Task<List<Review>> Get() =>
        await _ReviewService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [Authorize]
    public async Task<ActionResult<Review>> Get(string id)
    {
        var Review = await _ReviewService.GetAsync(id);

        if (Review is null)
        {
            return NotFound();
        }

        return Review;
    }

    [HttpGet("{Stars}")]
    [Authorize]
    public async Task<List<Review>> GetS(decimal stars) =>
        await _ReviewService.GetAsyncS(stars);

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(Review newReview)
    {
        await _ReviewService.CreateAsync(newReview);

        return CreatedAtAction(nameof(Get), new { id = newReview.Id }, newReview);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    public async Task<IActionResult> Update(string id, Review updatedReview)
    {
        //TODO:
        //Updater without need of all data
        var Review = await _ReviewService.GetAsync(id);
        if (Review is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id;length(24)}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var Review = await _ReviewService.GetAsync(id);
        if (Review is null)
        {
            return NotFound();
        }

        await _ReviewService.RemoveAsync(id);

        return NoContent();
    }
}
