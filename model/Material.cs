using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olya.model;

public class Material
{
    [Key]
    public Guid MaterialId { get; set; }

    [ForeignKey("Course")]
    public Guid CourseId { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; }

    public string Content { get; set; }

    public Course Course { get; set; }
}