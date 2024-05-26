using System.ComponentModel.DataAnnotations;

namespace Olya.dto;

public class UpdateEnrollmentDto
{
    [Required]
    public DateTime DateEnrolled { get; set; }
}