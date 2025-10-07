using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class TimeTableRepository : RepositoryBase<TimeTable>, ITimeTable
{
    public TimeTableRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
