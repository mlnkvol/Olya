namespace Olya.dto;

public class CourseDto
{
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Subject { get; set; }
    public Guid InstructorId { get; set; }
}