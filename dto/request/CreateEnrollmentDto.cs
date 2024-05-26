using System.ComponentModel.DataAnnotations;

namespace Olya.dto;

public class CreateEnrollmentDto
{
    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid CourseId { get; set; }

    [Required]
    public DateTime DateEnrolled { get; set; }
}