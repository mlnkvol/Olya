using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olya.dto;
using Olya.model;

namespace Olya.controller;

[ApiController]
[Route("api/[controller]")]
public class AssignmentSubmissionsController : ControllerBase
{
    private readonly CoursePlatformContext _context;

    public AssignmentSubmissionsController(CoursePlatformContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentSubmissionDto>>> GetAssignmentSubmissions()
    {
        var submissions = await _context.AssignmentSubmissions
            .Select(submission => new AssignmentSubmissionDto
            {
                SubmissionId = submission.SubmissionId,
                AssignmentId = submission.AssignmentId,
                StudentId = submission.StudentId,
                SubmittedOn = submission.SubmittedOn,
                Score = submission.Score,
                Content = submission.Content
            })
            .ToListAsync();

        return Ok(submissions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentSubmissionDto>> GetAssignmentSubmission(Guid id)
    {
        var submission = await _context.AssignmentSubmissions
            .Select(submission => new AssignmentSubmissionDto
            {
                SubmissionId = submission.SubmissionId,
                AssignmentId = submission.AssignmentId,
                StudentId = submission.StudentId,
                SubmittedOn = submission.SubmittedOn,
                Score = submission.Score,
                Content = submission.Content
            })
            .FirstOrDefaultAsync(s => s.SubmissionId == id);

        if (submission == null)
        {
            return NotFound();
        }

        return Ok(submission);
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentSubmissionDto>> CreateAssignmentSubmission(CreateAssignmentSubmissionDto createSubmissionDto)
    {
        var submission = new AssignmentSubmission
        {
            SubmissionId = Guid.NewGuid(),
            AssignmentId = createSubmissionDto.AssignmentId,
            StudentId = createSubmissionDto.StudentId,
            SubmittedOn = createSubmissionDto.SubmittedOn,
            Score = createSubmissionDto.Score,
            Content = createSubmissionDto.Content
        };

        _context.AssignmentSubmissions.Add(submission);
        await _context.SaveChangesAsync();

        var submissionDto = new AssignmentSubmissionDto
        {
            SubmissionId = submission.SubmissionId,
            AssignmentId = submission.AssignmentId,
            StudentId = submission.StudentId,
            SubmittedOn = submission.SubmittedOn,
            Score = submission.Score,
            Content = submission.Content
        };

        return CreatedAtAction(nameof(GetAssignmentSubmission), new { id = submission.SubmissionId }, submissionDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssignmentSubmission(Guid id, UpdateAssignmentSubmissionDto updateSubmissionDto)
    {
        var submission = await _context.AssignmentSubmissions.FindAsync(id);

        if (submission == null)
        {
            return NotFound();
        }

        submission.Score = updateSubmissionDto.Score;
        submission.Content = updateSubmissionDto.Content;

        _context.Entry(submission).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignmentSubmission(Guid id)
    {
        var submission = await _context.AssignmentSubmissions.FindAsync(id);

        if (submission == null)
        {
            return NotFound();
        }

        _context.AssignmentSubmissions.Remove(submission);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}