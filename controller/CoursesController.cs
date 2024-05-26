using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olya.dto;
using Olya.model;

namespace Olya.controller;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly CoursePlatformContext _context;

    public CoursesController(CoursePlatformContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        var courses = await _context.Courses
            .Select(course => new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description,
                Subject = course.Subject,
                InstructorId = course.InstructorId
            })
            .ToListAsync();

        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(Guid id)
    {
        var course = await _context.Courses
            .Select(course => new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description,
                Subject = course.Subject,
                InstructorId = course.InstructorId
            })
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (course == null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(SaveCourseDto createCourseDto)
    {
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = createCourseDto.Name,
            Description = createCourseDto.Description,
            Subject = createCourseDto.Subject,
            InstructorId = createCourseDto.InstructorId
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var courseDto = new CourseDto
        {
            CourseId = course.CourseId,
            Name = course.Name,
            Description = course.Description,
            Subject = course.Subject,
            InstructorId = course.InstructorId
        };

        return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, courseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(Guid id, SaveCourseDto updateCourseDto)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        course.Name = updateCourseDto.Name;
        course.Description = updateCourseDto.Description;
        course.Subject = updateCourseDto.Subject;
        course.InstructorId = updateCourseDto.InstructorId;

        _context.Entry(course).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}