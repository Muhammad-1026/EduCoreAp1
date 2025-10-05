using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Courses.Repositories;
using EduCoreApi.Application.Feature.Groups.Repositories;
using EduCoreApi.Application.Feature.Specialitys.Repositories;
using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Application.Feature.Teachers.Repositories;
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

        return services;
    }
}