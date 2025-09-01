using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
{
    public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
