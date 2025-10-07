using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EduCoreApi.Infrastructure.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ISpecialityRepository, SpecialityRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ICourseSubjectRepository, CourseSubjectRepository>();
        services.AddScoped<ICourseTeacherRepository, CourseTeacherRepository>();

        return services;
    }
}