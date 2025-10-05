using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class CourseSubject : Entity
{
    public Guid CourseId { get; private set; }
    public Course Course { get; private set; } = default!;

    public Guid SubjectId { get; private set; }
    public Subject Subject { get; private set; } = default!;

    public int Credits { get; private set; }
    public int Hours { get; private set; }

    public CourseSubject(Guid courseId, Guid subjectId, int credits, int hours, Guid createdBy) : base(createdBy)
    {
        CourseId = courseId;
        SubjectId = subjectId;
        SetCredits(credits);
        SetHours(hours);
    }

    public void SetCourseIds(Guid courseId)
    {
        if (courseId == Guid.Empty)
            throw new BussinessLogicException(CourseSubjectErrors.CourseIdIsInvalid);

        CourseId = courseId;
    }

    public void SetSubjectIds(Guid subjectId)
    {
        if (subjectId == Guid.Empty)
            throw new BussinessLogicException(CourseSubjectErrors.SubjectIdIsInvalid);

        SubjectId = subjectId;
    }

    public void SetCredits(int credits)
    {
        if (credits <= 0)
            throw new BussinessLogicException(CourseSubjectErrors.CreditsMustBePositive);

        Credits = credits;
    }

    public void SetHours(int hours)
    {
        if (hours <= 0)
            throw new BussinessLogicException(CourseSubjectErrors.HoursMustBePositive);

        Hours = hours;
    }

    public static class CourseSubjectErrors
    {
        public static readonly Error CourseIdIsInvalid = new(
            "CourseSubject.CourseIdIsInvalid",
            "Course ID указан неверно."
        );

        public static readonly Error SubjectIdIsInvalid = new(
            "CourseSubject.SubjectIdIsInvalid",
            "Subject ID указан неверно."
        );

        public static readonly Error CreditsMustBePositive = new(
            "CourseSubject.CreditsMustBePositive",
            "Количество кредитов должно быть больше нуля."
        );

        public static readonly Error HoursMustBePositive = new(
            "CourseSubject.HoursMustBePositive",
            "Количество часов должно быть больше нуля."
        );
    }
}