using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Domain.Models;
using Mapster;

namespace EduCoreApi.Application.Common.Mappings;

public class TeacherMapster : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Teacher, GetTeacherDto>();
        config.NewConfig<CreateTeacherDto, Teacher>();
        config.NewConfig<UpdateTeacherDto, Teacher>();
    }
}