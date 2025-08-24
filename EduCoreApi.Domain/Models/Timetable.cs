using EduCoreApi.Domain.Common;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

public class Timetable : Entity
{
    public Guid SubjectId { get; private set; }
    public Subject Subject { get; private set; } = default!;

    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = default!;

    public DayOfWeek DayOfWeek { get; private set; }

    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    public Timetable(Guid subjectId, Guid groupId, DayOfWeek dayOfWeek,TimeOnly startTime, TimeOnly endTime, Guid createdBy) : base(createdBy) 
    {
        SetSubjectId(subjectId);
        SetGroupId(groupId);
        SetDayOfWeek(dayOfWeek);
        SetStartTime(startTime);
        SetEndTime(endTime);
    }


    public void SetSubjectId(Guid subjectId)
    {
        if (subjectId == Guid.Empty)
            throw new BussinessLogicException(TimetableErrors.SubjectIdCantBeNull);

        SubjectId = subjectId;
    }

    public void SetGroupId(Guid groupId)
    {
        if (groupId == Guid.Empty)
            throw new BussinessLogicException(TimetableErrors.GroupIdCantBeNull);

        GroupId = groupId;
    }

    public void SetDayOfWeek(DayOfWeek dayOfWeek)
    {
        if (dayOfWeek == default)
            throw new BussinessLogicException(TimetableErrors.DayOfWeekCantBeNull);

        DayOfWeek = dayOfWeek;
    }

    public void SetStartTime(TimeOnly startTime)
    {
        if (startTime < new TimeOnly(7, 0) || startTime > new TimeOnly(20, 0))
            throw new BussinessLogicException(TimetableErrors.StartTimeInvalid);

        StartTime = startTime;
    }

    public void SetEndTime(TimeOnly endTime)
    {
        if (endTime == default)
            throw new BussinessLogicException(TimetableErrors.EndTimeCantBeNull);

        if (endTime <= StartTime)
            throw new BussinessLogicException(TimetableErrors.EndTimeMustBeGreaterThanStartTime);

        EndTime = endTime;
    }

    public static class TimetableErrors
    {
        public static readonly Error SubjectIdCantBeNull = new(
            "Timetable.SubjectIdCantBeNull",
            "Идентификатор предмета обязателен."
        );

        public static readonly Error GroupIdCantBeNull = new(
            "Timetable.GroupIdCantBeNull",
            "Идентификатор группы обязателен."
        );

        public static readonly Error DayOfWeekCantBeNull = new(
            "Timetable.DayOfWeekCantBeNull",
            "День недели обязателен."
        );

        public static readonly Error StartTimeInvalid = new(
            "Timetable.StartTimeInvalid",
            "Время начала должно быть в диапазоне с 07:00 до 20:00."
        );

        public static readonly Error EndTimeCantBeNull = new(
            "Timetable.EndTimeCantBeNull",
            "Время окончания обязательно."
        );

        public static readonly Error EndTimeMustBeGreaterThanStartTime = new(
            "Timetable.EndTimeMustBeGreaterThanStartTime",
            "Время окончания должно быть позже времени начала."
        );
    }
}