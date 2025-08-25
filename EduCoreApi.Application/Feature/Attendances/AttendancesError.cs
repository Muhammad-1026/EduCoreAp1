using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Attendances;

public class AttendancesError
{
    public static readonly Error NotFound = new(
        "Attendance.NotFound",
        "Посещаемость не найдена"
    );
}