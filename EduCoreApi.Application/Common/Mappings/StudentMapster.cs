using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Domain.Models;
using Mapster;

namespace EduCoreApi.Application.Common.Mappings;

public class StudentMapster : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Student, GetStudentDto>();
        config.NewConfig<CreateStudentDto, Student>();
        config.NewConfig<UpdateStudentDto, Student>();
    }
}