using System.ComponentModel.DataAnnotations;

namespace Olya.dto;

public class SaveCourseDto
{
    [Required, StringLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    public Guid InstructorId { get; set; }
}