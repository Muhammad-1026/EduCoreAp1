using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Groups;

public class GropError
{
    public static readonly Error NotFound = new(
        "Group.NotFound",
        "Группа не найдена"
    );
}
