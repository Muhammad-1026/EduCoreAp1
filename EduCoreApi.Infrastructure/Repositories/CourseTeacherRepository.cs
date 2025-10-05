using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class CourseTeacherRepository : RepositoryBase<CourseTeacher>, ICourseTeacherRepository
{
    public CourseTeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}