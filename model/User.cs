using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Olya.model;

public class User : IdentityUser<Guid>
{
    [Required, StringLength(100)]
    public string Name { get; set; }
    
    [Required, StringLength(100)]
    public string Surname { get; set; }
    
    public ICollection<Course> Courses { get; set; } 
    public ICollection<Enrollment> Enrollments { get; set; } 
    public ICollection<Assignment> AssignmentsCreated { get; set; } 
    public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; }
}