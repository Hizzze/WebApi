using App.Models.Properties;
using App.Models.PropertyDtos;
using Microsoft.AspNetCore.Mvc;

namespace App.Abstractions;

public interface IPropertiesRepository
{
    public Task<PropertyDto> Create([FromBody] PropertyCreateDto dto, List<IFormFile> images);
    public Task<List<PropertyDto>> GetAll();
    public Task<PropertyDto> Update(Guid id, PropertyCreateDto dto);
    public Task<bool> Delete(Guid id);
}