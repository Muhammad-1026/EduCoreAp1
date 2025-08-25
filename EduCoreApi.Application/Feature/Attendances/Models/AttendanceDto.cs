namespace EduCoreApi.Application.Feature.Attendances.Models;

public class AttendanceDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
}
