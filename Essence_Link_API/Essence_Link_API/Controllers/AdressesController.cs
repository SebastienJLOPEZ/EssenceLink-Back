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
public class AdressesController : Controller
{
    private readonly AdressesService _AdressesService;

    public AdressesController(AdressesService AdressesService) =>
        _AdressesService = AdressesService;

    [HttpGet]
    public async Task<List<Adresses>> Get() =>
        await _AdressesService.GetAsync();

    [HttpGet("adress/{id}")]
    public async Task<ActionResult<Adresses>> Get(string id)
    {
        var Adresses = await _AdressesService.GetAsync(id);

        if (Adresses is null)
        {
            return NotFound();
        }

        return Adresses;
    }

    [HttpGet("{uid}")]
    public async Task<List<Adresses>> GetUList(string uid) =>
        await _AdressesService.GetAsyncUList(uid);

    [HttpPost]
    public async Task<IActionResult> Post(Adresses newAdresses)
    {
        await _AdressesService.CreateAsync(newAdresses);

        return CreatedAtAction(nameof(Get), new { id = newAdresses.Id }, newAdresses);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Adresses updatedAdresses)
    {
        //TODO:
        //Updater without need of all data
        var Adresses = await _AdressesService.GetAsync(id);
        if (Adresses is null)
        {
            return NotFound();
        }

        updatedAdresses.Id = Adresses.Id;

        await _AdressesService.UpdateAsync(id, updatedAdresses);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var Adresses = await _AdressesService.GetAsync(id);
        if (Adresses is null)
        {
            return NotFound();
        }

        await _AdressesService.RemoveAsync(id);

        return NoContent();
    }
}
