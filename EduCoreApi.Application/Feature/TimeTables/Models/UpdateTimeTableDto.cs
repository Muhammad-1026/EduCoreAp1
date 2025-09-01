namespace EduCoreApi.Application.Feature.TimeTables.Models;

public class UpdateTimeTableDto
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid SubjectId { get; set; }
    public Guid GroupId { get; set; }
}