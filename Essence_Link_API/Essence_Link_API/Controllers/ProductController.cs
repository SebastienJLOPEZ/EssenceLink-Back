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

public class ProductController : Controller
{
    private readonly ProductService _ProductService;

    public ProductController(ProductService ProductService) =>
        _ProductService = ProductService;

    [HttpGet]
    public async Task<List<Product>> Get() =>
        await _ProductService.GetAsync();

    [HttpGet("{id:length(24)}")] //For when we want to see one precise product
    public async Task<ActionResult<Product>> Get(string id)
    {
        var Product = await _ProductService.GetAsync(id);

        if (Product is null)
        {
            return NotFound();
        }

        return Product;
    }

    // Get by Product Type

    [HttpGet("Hydrolat/")]
    public async Task<List<Product>> GetHydro() =>
        await _ProductService.GetAsyncHydro();

    [HttpGet("TnP/")]
    public async Task<List<Product>> GetTnP() =>
        await _ProductService.GetAsyncTnP();

    [HttpGet("Gemmothérapie/")]
    public async Task<List<Product>> GetGem() =>
        await _ProductService.GetAsyncGem();

    [HttpGet("Aromate/")]
    public async Task<List<Product>> GetArom() =>
        await _ProductService.GetAsyncArom();

    [HttpGet("Boisson/")] //Will be change later, with 5 others but with precision of what I want to send.
    public async Task<List<Product>> GetDrink() =>
        await _ProductService.GetAsyncDrink();

    // Get by Product Subtype

    [HttpGet("Hydrolat/{subtype}")] //Will be change later, with 6 others but with precision of what I want to send.
    public async Task<List<Product>> GetHydroST(string subtype) =>
        await _ProductService.GetAsyncHydroST(subtype);

    [HttpGet("Boisson/{subtype}")] //Will be change later, with 6 others but with precision of what I want to send.
    public async Task<List<Product>> GetTnPST(string subtype) =>
        await _ProductService.GetAsyncTnPST(subtype);

    [HttpGet("Boisson/{subtype}")] //Will be change later, with 6 others but with precision of what I want to send.
    public async Task<List<Product>> GetGemST(string subtype) =>
        await _ProductService.GetAsyncGemST(subtype);

    [HttpGet("Boisson/{subtype}")] //Will be change later, with 6 others but with precision of what I want to send.
    public async Task<List<Product>> GetAromST(string subtype) =>
        await _ProductService.GetAsyncAromST(subtype);

    [HttpGet("Boisson/{subtype}")] //Will be change later, with 6 others but with precision of what I want to send.
    public async Task<List<Product>> GetDrinkNA(string subtype) =>
        await _ProductService.GetAsyncDrinkST(subtype);

    // Get by Research Term

    [HttpGet("Search/{SearchTerm}")]
    public async Task<List<Product>> GetN(string searchTerm) =>
        await _ProductService.GetAsyncN(searchTerm);

    [HttpPost]
    public async Task<IActionResult> Post(Product newProduct)
    {
        //TODO :
        //Add checker of type, making sure it's an autorize type
        await _ProductService.CreateAsync(newProduct);

        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updatedProduct)
    {
        //TODO:
        //Updater without need of all data
        var Product = await _ProductService.GetAsync(id);
        if (Product is null)
        {
            return NotFound();
        }
        updatedProduct.Id = Product.Id;

        await _ProductService.UpdateAsync(id, updatedProduct);

        return NoContent();
    }

    [HttpDelete("{id;length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        //TODO:
        //Change it so it doesn't delete but hash it instead
        var Product = await _ProductService.GetAsync(id);
        if (Product is null)
        {
            return NotFound();
        }

        await _ProductService.RemoveAsync(id);

        return NoContent();
    }
}
