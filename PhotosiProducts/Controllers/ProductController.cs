using Microsoft.AspNetCore.Mvc;
using PhotosiProducts.Service;

namespace PhotosiProducts.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _productService.GetAsync());
    }
}
