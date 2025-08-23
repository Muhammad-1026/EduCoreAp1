using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Grades;

public class GradeError
{
    public static readonly Error NotFound = new(
        "Grade.NotFound",
        "Оценка не найдена"
    );
}