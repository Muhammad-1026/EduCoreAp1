using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class GroupRepository : RepositoryBase<Group>, IGroupRepository
{
    public GroupRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}