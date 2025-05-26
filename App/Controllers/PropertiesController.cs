using App.Abstractions;
using App.Models.PropertyDtos;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertiesRepository _propertiesRepository;
    
    
    public PropertiesController(IPropertiesRepository propertiesRepository)
    {
        _propertiesRepository = propertiesRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PropertyDto>>> GetAll()
    {
        var properties = await _propertiesRepository.GetAll();
        return Ok(properties);
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateProperty([FromForm] PropertyCreateDto dto, [FromForm] List<IFormFile> images)
    {
        var property = await _propertiesRepository.Create(dto, images);
        return CreatedAtAction(nameof(CreateProperty), new { id = property.Id }, property);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] PropertyCreateDto dto)
    {
        await _propertiesRepository.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        await _propertiesRepository.Delete(id);
        return NoContent();
    }
}