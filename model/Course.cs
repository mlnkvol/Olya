using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olya.model;

public class Course
{
    [Key]
    public Guid CourseId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Subject { get; set; }

    [ForeignKey("Instructor")]
    public Guid InstructorId { get; set; }

    public User Instructor { get; set; }
    public ICollection<Material> Materials { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
}