namespace Olya.dto;

public class EnrollmentDto
{
    public Guid EnrollmentId { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime DateEnrolled { get; set; }
}