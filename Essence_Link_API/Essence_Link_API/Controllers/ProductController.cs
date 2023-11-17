﻿//using Microsoft.AspNetCore.Http;
using Essence_Link_API.Models;
using Essence_Link_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Essence_Link_API.Controllers;
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

    [HttpGet("Type")]
    public async Task<List<Product>> GetT(string type) =>
        await _ProductService.GetAsyncT(type);

    [HttpGet("SearchTerm")]
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
