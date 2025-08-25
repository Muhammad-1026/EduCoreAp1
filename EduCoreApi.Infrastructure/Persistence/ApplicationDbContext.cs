using EduCoreApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    DbSet<Student> Students { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<Grade> Grades { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<Speciality> Specialties { get; set; }
    DbSet<Attendance> Attendances { get; set; }
    DbSet<Timetable> Timetables { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<CourseTeacher> CourseTeachers { get; set; }
    DbSet<CourseSubject> CourseSubjects { get; set; }
    DbSet<CourseStudent> CourseStudents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}