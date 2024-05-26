using System.ComponentModel.DataAnnotations;

namespace Olya.dto;

public class CreateAssignmentDto
{
    [Required]
    public Guid CourseId { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public Guid InstructorId { get; set; }

    [Required]
    public int MaxScore { get; set; }
}