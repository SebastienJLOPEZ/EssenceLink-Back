//using Microsoft.AspNetCore.Http;
using Essence_Link_API.Models;
using Essence_Link_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Essence_Link_API.Controllers;
public class ProductPictureController : Controller
{
    private readonly ProductPictureService _ProductPictureService;

    public ProductPictureController(ProductPictureService ProductPictureService) =>
        _ProductPictureService = ProductPictureService;

    [HttpGet]
    public async Task<List<ProductPicture>> Get() =>
        await _ProductPictureService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ProductPicture>> Get(string id)
    {
        var ProductPicture = await _ProductPictureService.GetAsync(id);

        if (ProductPicture is null)
        {
            return NotFound();
        }

        return ProductPicture;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductPicture newProductPicture)
    {
        await _ProductPictureService.CreateAsync(newProductPicture);

        return CreatedAtAction(nameof(Get), new { id = newProductPicture.Id }, newProductPicture);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, ProductPicture updatedProductPicture)
    {
        //TODO:
        //Updater without need of all data
        var ProductPicture = await _ProductPictureService.GetAsync(id);
        if (ProductPicture is null)
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
        var ProductPicture = await _ProductPictureService.GetAsync(id);
        if (ProductPicture is null)
        {
            return NotFound();
        }

        await _ProductPictureService.RemoveAsync(id);

        return NoContent();
    }
}
