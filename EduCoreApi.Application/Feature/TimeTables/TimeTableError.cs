using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.TimeTables;

public class TimeTableError
{
    public static readonly Error NotFound = new(
        "TimeTable.NotFound",
        "Расписание не найдено"
    );
}