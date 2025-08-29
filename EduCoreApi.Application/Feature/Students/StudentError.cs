using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Students;

public class StudentError
{
    public static readonly Error NotFound = new(
        "Student.NotFound",
        "Студент не найден"
    );
}