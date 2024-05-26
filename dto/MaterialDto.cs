namespace Olya.dto;

public class MaterialDto
{
    public Guid MaterialId { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}