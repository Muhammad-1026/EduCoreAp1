using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Attendance : Entity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = default!;

    public Guid SubjectId { get; private set; }
    public Subject Subject { get; private set; } = default!;

    public DateTime Date { get; private set; }
    public bool IsPresent { get; private set; }

    public Attendance(Guid studentId, Guid subjectId, DateTime date, bool isPresent, Guid createdBy) : base(createdBy)
    {
        SetStudentId(studentId);
        SetSubjectId(subjectId);
        SetDate(date);
        SetIsPresent(isPresent);
    }

    public void SetStudentId(Guid studentId)
    {
        if (studentId == Guid.Empty)
            throw new BussinessLogicException(AttendanceErrors.StudentIdCantBeNull);

        StudentId = studentId;
    }

    public void SetSubjectId(Guid subjectId)
    {
        if (subjectId == Guid.Empty)
            throw new BussinessLogicException(AttendanceErrors.SubjectIdCantBeNull);

        SubjectId = subjectId;
    }

    public void SetDate(DateTime date)
    {
        if (date == default)
            throw new BussinessLogicException(AttendanceErrors.DateCantBeNull);

        Date = date;
    }

    public void SetIsPresent(bool isPresent)
    {
        IsPresent = isPresent;
    }

    public static class AttendanceErrors
    {
        public static readonly Error StudentIdCantBeNull = new(
            "Attendance.StudentIdCantBeNull",
            "Студент обязателен."
        );

        public static readonly Error SubjectIdCantBeNull = new(
            "Attendance.SubjectIdCantBeNull",
            "Предмет обязателен."
        );

        public static readonly Error DateCantBeNull = new(
            "Attendance.DateCantBeNull",
            "Дата не может быть пустой."
        );
    }
}