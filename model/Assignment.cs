using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olya.model;

public class Assignment
{
    [Key]
    public Guid AssignmentId { get; set; }

    [ForeignKey("Course")]
    public Guid CourseId { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    [ForeignKey("Instructor")]
    public Guid InstructorId { get; set; }

    public int MaxScore { get; set; }

    public Course Course { get; set; }
    public User Instructor { get; set; }
    public ICollection<AssignmentSubmission> Submissions { get; set; }
}