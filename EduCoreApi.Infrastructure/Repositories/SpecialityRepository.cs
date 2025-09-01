using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Feature.Specialitys.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

internal sealed class SpecialityRepository : RepositoryBase<Speciality>, ISpecialityRepository
{
    public SpecialityRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
