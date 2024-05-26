namespace Olya.dto;

public class AssignmentSubmissionDto
{
    public Guid SubmissionId { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime SubmittedOn { get; set; }
    public int Score { get; set; }
    public string Content { get; set; }
}