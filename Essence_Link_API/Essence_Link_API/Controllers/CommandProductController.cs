//using Microsoft.AspNetCore.Http;
using Essence_Link_API.Models;
using Essence_Link_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Essence_Link_API.Controllers;
public class CommandProductController : Controller
{
    private readonly CommandProductService _CommandProductService;

    public CommandProductController(CommandProductService CommandProductService) =>
        _CommandProductService = CommandProductService;

    [HttpGet]
    public async Task<List<CommandProduct>> Get() =>
        await _CommandProductService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<CommandProduct>> Get(string id)
    {
        var CommandProduct = await _CommandProductService.GetAsync(id);

        if (CommandProduct is null)
        {
            return NotFound();
        }

        return CommandProduct;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CommandProduct newCommandProduct)
    {
        await _CommandProductService.CreateAsync(newCommandProduct);

        return CreatedAtAction(nameof(Get), new { id = newCommandProduct.Id }, newCommandProduct);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, CommandProduct updatedCommandProduct)
    {
        //TODO:
        //Updater without need of all data
        var CommandProduct = await _CommandProductService.GetAsync(id);
        if (CommandProduct is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id;length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var CommandProduct = await _CommandProductService.GetAsync(id);
        if (CommandProduct is null)
        {
            return NotFound();
        }

        await _CommandProductService.RemoveAsync(id);

        return NoContent();
    }
}
