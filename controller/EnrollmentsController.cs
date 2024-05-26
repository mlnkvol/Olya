using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olya.dto;
using Olya.model;

namespace Olya.controller;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly CoursePlatformContext _context;

    public EnrollmentsController(CoursePlatformContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollments()
    {
        var enrollments = await _context.Enrollments
            .Select(enrollment => new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                DateEnrolled = enrollment.DateEnrolled
            })
            .ToListAsync();

        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentDto>> GetEnrollment(Guid id)
    {
        var enrollment = await _context.Enrollments
            .Select(enrollment => new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                DateEnrolled = enrollment.DateEnrolled
            })
            .FirstOrDefaultAsync(e => e.EnrollmentId == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return Ok(enrollment);
    }

    [HttpPost]
    public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(CreateEnrollmentDto createEnrollmentDto)
    {
        var enrollment = new Enrollment
        {
            EnrollmentId = Guid.NewGuid(),
            StudentId = createEnrollmentDto.StudentId,
            CourseId = createEnrollmentDto.CourseId,
            DateEnrolled = createEnrollmentDto.DateEnrolled
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        var enrollmentDto = new EnrollmentDto
        {
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = enrollment.StudentId,
            CourseId = enrollment.CourseId,
            DateEnrolled = enrollment.DateEnrolled
        };

        return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentId }, enrollmentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEnrollment(Guid id, UpdateEnrollmentDto updateEnrollmentDto)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment == null)
        {
            return NotFound();
        }

        enrollment.DateEnrolled = updateEnrollmentDto.DateEnrolled;

        _context.Entry(enrollment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrollment(Guid id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment == null)
        {
            return NotFound();
        }

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}