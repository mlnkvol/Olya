using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olya.dto;
using Olya.model;

namespace Olya.controller;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly CoursePlatformContext _context;

    public AssignmentsController(CoursePlatformContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAssignments()
    {
        var assignments = await _context.Assignments
            .Select(assignment => new AssignmentDto
            {
                AssignmentId = assignment.AssignmentId,
                CourseId = assignment.CourseId,
                Title = assignment.Title,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                InstructorId = assignment.InstructorId,
                MaxScore = assignment.MaxScore
            })
            .ToListAsync();

        return Ok(assignments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentDto>> GetAssignment(Guid id)
    {
        var assignment = await _context.Assignments
            .Select(assignment => new AssignmentDto
            {
                AssignmentId = assignment.AssignmentId,
                CourseId = assignment.CourseId,
                Title = assignment.Title,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                InstructorId = assignment.InstructorId,
                MaxScore = assignment.MaxScore
            })
            .FirstOrDefaultAsync(a => a.AssignmentId == id);

        if (assignment == null)
        {
            return NotFound();
        }

        return Ok(assignment);
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentDto>> CreateAssignment(CreateAssignmentDto createAssignmentDto)
    {
        var assignment = new Assignment
        {
            AssignmentId = Guid.NewGuid(),
            CourseId = createAssignmentDto.CourseId,
            Title = createAssignmentDto.Title,
            Description = createAssignmentDto.Description,
            DueDate = createAssignmentDto.DueDate,
            InstructorId = createAssignmentDto.InstructorId,
            MaxScore = createAssignmentDto.MaxScore
        };

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        var assignmentDto = new AssignmentDto
        {
            AssignmentId = assignment.AssignmentId,
            CourseId = assignment.CourseId,
            Title = assignment.Title,
            Description = assignment.Description,
            DueDate = assignment.DueDate,
            InstructorId = assignment.InstructorId,
            MaxScore = assignment.MaxScore
        };

        return CreatedAtAction(nameof(GetAssignment), new { id = assignment.AssignmentId }, assignmentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssignment(Guid id, UpdateAssignmentDto updateAssignmentDto)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound();
        }

        assignment.Title = updateAssignmentDto.Title;
        assignment.Description = updateAssignmentDto.Description;
        assignment.DueDate = updateAssignmentDto.DueDate;
        assignment.MaxScore = updateAssignmentDto.MaxScore;

        _context.Entry(assignment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(Guid id)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound();
        }

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}