using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olya.dto;
using Olya.model;

namespace Olya.controller;

[ApiController]
[Route("api/[controller]")]
public class MaterialsController : ControllerBase
{
    private readonly CoursePlatformContext _context;

    public MaterialsController(CoursePlatformContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterials()
    {
        var materials = await _context.Materials
            .Select(material => new MaterialDto
            {
                MaterialId = material.MaterialId,
                CourseId = material.CourseId,
                Title = material.Title,
                Content = material.Content
            })
            .ToListAsync();

        return Ok(materials);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDto>> GetMaterial(Guid id)
    {
        var material = await _context.Materials
            .Select(material => new MaterialDto
            {
                MaterialId = material.MaterialId,
                CourseId = material.CourseId,
                Title = material.Title,
                Content = material.Content
            })
            .FirstOrDefaultAsync(m => m.MaterialId == id);

        if (material == null)
        {
            return NotFound();
        }

        return Ok(material);
    }

    [HttpPost]
    public async Task<ActionResult<MaterialDto>> CreateMaterial(SaveMaterialDto createMaterialDto)
    {
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            CourseId = createMaterialDto.CourseId,
            Title = createMaterialDto.Title,
            Content = createMaterialDto.Content
        };

        _context.Materials.Add(material);
        await _context.SaveChangesAsync();

        var materialDto = new MaterialDto
        {
            MaterialId = material.MaterialId,
            CourseId = material.CourseId,
            Title = material.Title,
            Content = material.Content
        };

        return CreatedAtAction(nameof(GetMaterial), new { id = material.MaterialId }, materialDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaterial(Guid id, SaveMaterialDto updateMaterialDto)
    {
        var material = await _context.Materials.FindAsync(id);

        if (material == null)
        {
            return NotFound();
        }

        material.Title = updateMaterialDto.Title;
        material.Content = updateMaterialDto.Content;

        _context.Entry(material).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaterial(Guid id)
    {
        var material = await _context.Materials.FindAsync(id);

        if (material == null)
        {
            return NotFound();
        }

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}