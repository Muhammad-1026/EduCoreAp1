using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Feature.Groups.Repositories;
using EduCoreApi.Infrastructure.Persistence;
using System.Text.RegularExpressions;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class GroupRepository : RepositoryBase<Group>, IGroupRepository
{
    public GroupRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
