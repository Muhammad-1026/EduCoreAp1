using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
