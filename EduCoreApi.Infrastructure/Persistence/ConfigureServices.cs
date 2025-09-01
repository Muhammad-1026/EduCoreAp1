using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Courses.Repositories;
using EduCoreApi.Application.Feature.Departments.Repositories;
using EduCoreApi.Application.Feature.Groups.Repositories;
using EduCoreApi.Application.Feature.Specialitys.Repositories;
using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Application.Feature.Teachers.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduCoreApi.Infrastructure.Persistence;

public class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var conntectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(conntectionString));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();       
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ISpecialityRepository, SpecialityRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}
