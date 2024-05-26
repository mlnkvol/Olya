using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olya.model;

public class Enrollment
{
    [Key]
    public Guid EnrollmentId { get; set; }

    [ForeignKey("Student")]
    public Guid StudentId { get; set; }

    [ForeignKey("Course")]
    public Guid CourseId { get; set; }

    public DateTime DateEnrolled { get; set; }

    public User Student { get; set; }
    public Course Course { get; set; }
}