using EduCoreApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    // database.EnsureCreated();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    DbSet<Student> Students { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<Speciality> Specialties { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<CourseTeacher> CourseTeachers { get; set; }
    DbSet<CourseSubject> CourseSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}