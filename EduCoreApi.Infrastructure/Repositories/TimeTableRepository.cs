using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Feature.TimeTables.Repositories;
using EduCoreApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class TimeTableRepository : RepositoryBase<TimeTable>, ITimeTable
{
    public TimeTableRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
