using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Domain.Models;
using Mapster;

namespace EduCoreApi.Application.Feature.Students;

public class StudentMapster : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Student, StudentDto>();
        config.NewConfig<CreateStudentDto, Student>();
        config.NewConfig<UpdateStudentDto, Student>();
    }
}