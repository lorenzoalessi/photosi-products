using Microsoft.AspNetCore.Mvc;
using PhotosiProducts.Dto;
using PhotosiProducts.Exceptions;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id < 1)
            return BadRequest("ID fornito non valido");

        return Ok(await _productService.GetByIdAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
    {
        if (id < 1)
            return BadRequest("ID fornito non valido");
        
        if (productDto.CategoryId < 1)
            return BadRequest("Dati del prodotto non validi");

        try
        {
            var result = await _productService.UpdateAsync(id, productDto);
            return Ok($"Prodotto con ID {result.Id} salvato successo");
        }
        catch (Exception e) when (e is CategoryException or ProductException)
        {
            return BadRequest($"Errore nella richiesta di inserimento: {e.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductDto productDto)
    {
        if (productDto.CategoryId < 1)
            return BadRequest("Dati del prodotto non validi");

        try
        {
            return Ok(await _productService.AddAsync(productDto));
        }
        catch (CategoryException e)
        {
            return BadRequest($"Errore nella richiesta di inserimento: {e.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id < 1)
            return BadRequest("ID fornito non valido");

        try
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return StatusCode(500, "Errore nella richiesta di eliminazione");
            
            return Ok($"Prodotto con ID {id} eliminato con successo");
        }
        catch (ProductException e)
        {
            return BadRequest($"Errore nella richiesta di eliminazione: {e.Message}");
        }
    }
}
