using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Departments;

public class DepartmentError
{
    public static readonly Error NotFound = new(
        "Department.NotFound",
        "Кафедра не найдена"
    );
}