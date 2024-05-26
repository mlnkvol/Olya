using System.ComponentModel.DataAnnotations;

namespace Olya.dto;

public class SaveMaterialDto
{
    [Required, StringLength(100)]
    public string Title { get; set; }
    
    [Required]
    public Guid CourseId { get; set; }

    public string Content { get; set; }
}