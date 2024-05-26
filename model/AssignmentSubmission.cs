using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olya.model;

public class AssignmentSubmission
{
    [Key]
    public Guid SubmissionId { get; set; }

    [ForeignKey("Assignment")]
    public Guid AssignmentId { get; set; }

    [ForeignKey("Student")]
    public Guid StudentId { get; set; }

    public DateTime SubmittedOn { get; set; }

    public int Score { get; set; }

    public string Content { get; set; }

    public Assignment Assignment { get; set; }
    public User Student { get; set; }
}