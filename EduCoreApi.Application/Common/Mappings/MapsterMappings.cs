using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Application.Feature.Speciality.Models;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Subjects.Models;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Feature.TimeTables.Models;
using EduCoreApi.Domain.Models;
using Mapster;

namespace EduCoreApi.Application.Common.Mappings;

public class MapsterMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Course, GetCourseDto>();
        config.NewConfig<CreateCourseDto, Course>();
        config.NewConfig<UpdateCourseDto, Course>();

        config.NewConfig<Department, GetDepartmentDto>();
        config.NewConfig<CreateDepartmentDto, Department>();
        config.NewConfig<UpdateDepartmentDto, Department>();

        config.NewConfig<Group, GetGroupDto>();
        config.NewConfig<CreateGroupDto, Group>();
        config.NewConfig<UpdateGroupDto, Group>();

        config.NewConfig<Speciality, GetSpecialityDto>();
        config.NewConfig<CreateSpecialityDto, Speciality>();
        config.NewConfig<UpdateSpecialityDto, Speciality>();

        config.NewConfig<Student, GetStudentDto>();
        config.NewConfig<CreateStudentDto, Student>();
        config.NewConfig<UpdateStudentDto, Student>();

        config.NewConfig<Subject, GetSubjectDto>();
        config.NewConfig<CreateSubjectDto, Subject>();
        config.NewConfig<UpdateSubjectDto, Subject>();

        config.NewConfig<Teacher, GetTeacherDto>();
        config.NewConfig<CreateTeacherDto, Teacher>();
        config.NewConfig<UpdateTeacherDto, Teacher>();

        config.NewConfig<TimeTable, GetTimeTableDto>();
        config.NewConfig<CreateTimeTableDto, TimeTable>();
        config.NewConfig<UpdateTimeTableDto, TimeTable>();
    }
}
