using Ardalis.Specification.EntityFrameworkCore;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Infrastructure.Persistence;

namespace EduCoreApi.Infrastructure.Repositories;

public  class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
