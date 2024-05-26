namespace Olya.dto;

public class AssignmentDto
{
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Guid InstructorId { get; set; }
    public int MaxScore { get; set; }
}
