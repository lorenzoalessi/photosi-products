using Microsoft.AspNetCore.Mvc;
using PhotosiProducts.Services;

namespace PhotosiProducts.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    
    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _productsService.GetAsync());
    }
}
