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

public class ProductTypeController : Controller
{
    private readonly ProductTypeService _ProductTypeService;

    public ProductTypeController(ProductTypeService ProductTypeService) =>
        _ProductTypeService = ProductTypeService;

    [HttpGet]
    //[Authorize("BaseAccess")]

    public async Task<List<ProductType>> Get() =>
        await _ProductTypeService.GetAsync();

    [HttpGet("{pid}")]
    //
    public async Task<ActionResult<ProductType>> Get(string pid)
    {
        var ProductType = await _ProductTypeService.GetAsync(pid);

        if (ProductType is null)
        {
            return NotFound();
        }

        return ProductType;
    }

    [HttpPost]
    
    public async Task<IActionResult> Post(ProductType newProductType)
    {
        await _ProductTypeService.CreateAsync(newProductType);

        return CreatedAtAction(nameof(Get), new { id = newProductType.Id }, newProductType);
    }

    [HttpPut("{id:length(24)}")]
    
    public async Task<IActionResult> Update(string id, ProductType updatedProductType)
    {
        //TODO:
        //Updater without need of all data
        var ProductType = await _ProductTypeService.GetAsync(id);
        if (ProductType is null)
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
        var ProductType = await _ProductTypeService.GetAsync(id);
        if (ProductType is null)
        {
            return NotFound();
        }

        await _ProductTypeService.RemoveAsync(id);

        return NoContent();
    }
}
