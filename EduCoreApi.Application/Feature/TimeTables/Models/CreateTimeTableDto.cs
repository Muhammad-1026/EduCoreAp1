namespace EduCoreApi.Application.Feature.TimeTables.Models;

public class CreateTimeTableDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid SubjectId { get; set; }
    public Guid GroupId { get; set; }
}
