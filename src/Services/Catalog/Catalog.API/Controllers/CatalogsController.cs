using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CatalogsController> _logger;

    public CatalogsController(IProductRepository productRepository, ILogger<CatalogsController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllProducts()
    {
        var data = await _productRepository.GetAllProductsAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductById(string id)
    {
        var data = await _productRepository.GetProductByIdAsync(id);
        if (data is null)
        {
            _logger.LogWarning($"Product with id: {id} not found.");
            return NotFound();
        }

        return Ok(data);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateNewProduct([FromBody] Product product)
    {
        await _productRepository.InsertProductAsync(product);
        return CreatedAtAction("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        var result = await _productRepository.UpdateProductAsync(product);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product is null)
        {
            _logger.LogWarning($"Product with id: {id} not found.");
            return NotFound();
        }
        var result = await _productRepository.DeleteProductAsync(product);
        return Ok(result);
    }
}