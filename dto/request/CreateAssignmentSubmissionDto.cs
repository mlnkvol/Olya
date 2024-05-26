using System.ComponentModel.DataAnnotations;

namespace Olya.dto;

public class CreateAssignmentSubmissionDto
{
    [Required]
    public Guid AssignmentId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public DateTime SubmittedOn { get; set; }

    public int Score { get; set; }

    public string Content { get; set; }
}