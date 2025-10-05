using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class CourseSubjectRepository : RepositoryBase<CourseSubject>, ICourseSubjectRepository
{
    public CourseSubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
