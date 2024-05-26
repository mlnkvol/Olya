using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Olya.model;

public class CoursePlatformContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{

    public DbSet<Material> Materials { get; set; } = default!;
    public DbSet<Enrollment> Enrollments { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; } = default!;
    public DbSet<Assignment> Assignments { get; set; } = default!;
    
    public CoursePlatformContext()
    {
        
    }
    
    public CoursePlatformContext(DbContextOptions<CoursePlatformContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Course)
            .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Instructor)
            .WithMany(i => i.AssignmentsCreated)
            .HasForeignKey(a => a.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}