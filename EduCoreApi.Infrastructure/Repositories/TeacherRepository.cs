using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal class TeacherRepository : RepositoryBase<Teacher>, ITeacherRepository
{
    public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
