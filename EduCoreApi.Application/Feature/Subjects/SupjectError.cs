using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Subjects;

public class SupjectError
{
    public static readonly Error NotFound = new(
        "Subject.NotFound",
        "Предмет не найден"
    );
}