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
public class CommandController : Controller
{
    private readonly CommandService _CommandService;

    public CommandController(CommandService CommandService) =>
        _CommandService = CommandService;

    [HttpGet]
    [Authorize]
    public async Task<List<Command>> Get() =>
        await _CommandService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [Authorize]
    public async Task<ActionResult<Command>> Get(string id)
    {
        var Command = await _CommandService.GetAsync(id);

        if (Command is null)
        {
            return NotFound();
        }

        return Command;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(Command newCommand)
    {
        await _CommandService.CreateAsync(newCommand);

        return CreatedAtAction(nameof(Get), new { id = newCommand.Id }, newCommand);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    public async Task<IActionResult> Update(string id, Command updatedCommand)
    {
        //TODO:
        //Updater without need of all data
        var Command = await _CommandService.GetAsync(id);
        if (Command is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id;length(24)}")]
    [Authorize("ClientAdminAccess")]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var Command = await _CommandService.GetAsync(id);
        if (Command is null)
        {
            return NotFound();
        }

        await _CommandService.RemoveAsync(id);

        return NoContent();
    }
}
