//using Microsoft.AspNetCore.Http;
using Essence_Link_API.Models;
using Essence_Link_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Essence_Link_API.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
[EnableCors("BaseAccess")]
public class UserController : Controller
{
    private readonly UserService _UserService;

    public UserController(UserService UserService) =>
        _UserService = UserService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _UserService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var User = await _UserService.GetAsync(id);

        if (User is null)
        {
            return NotFound();
        }

        return User;
    }

    

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {

        await _UserService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        //TODO:
        //Updater without need of all data
        var User = await _UserService.GetAsync(id);
        if (User is null)
        {
            return NotFound();
        }

        updatedUser.Id = User.Id;

        await _UserService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var User = await _UserService.GetAsync(id);
        if (User is null)
        {
            return NotFound();
        }

        await _UserService.RemoveAsync(id);

        return NoContent();
    }
}
