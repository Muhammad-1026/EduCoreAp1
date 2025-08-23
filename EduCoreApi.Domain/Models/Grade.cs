using EduCoreApi.Domain.Common;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

public class Grade : Entity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = default!;

    public Guid SubjectId { get; private set; }
    public Subject Subject { get; private set; } = default!;

    public int Value { get; private set; }
    public GradeType Type { get; private set; }

    public Grade(Guid studentId, Guid courseSubjectId, int value, GradeType type, Guid createdBy)
        : base(createdBy)
    {
        SetStudentId(studentId);
        SetCourseSubjectId(courseSubjectId);
        SetType(type);
        SetValue(value);
    }

    public void SetStudentId(Guid studentId)
    {
        if (studentId == Guid.Empty)
            throw new BussinessLogicException(GradeErrors.StudentIdCantBeNull);

        StudentId = studentId;
    }

    public void SetCourseSubjectId(Guid courseSubjectId)
    {
        if (courseSubjectId == Guid.Empty)
            throw new BussinessLogicException(GradeErrors.CourseSubjectIdCantBeNull);

        CourseSubjectId = courseSubjectId;
    }

    public void SetType(GradeType type)
    {
        if (!Enum.IsDefined(typeof(GradeType), type))
            throw new BussinessLogicException(GradeErrors.TypeIsInvalid);

        Type = type;
    }

    public void SetValue(int value)
    {
        switch (Type)
        {
            case GradeType.Daily:
                if (value < 0 || value > 10)
                    throw new BussinessLogicException(GradeErrors.DailyGradeMustBeBetween0And10);
                break;

            case GradeType.Midterm:
                if (value < 0 || value > 100)
                    throw new BussinessLogicException(GradeErrors.MidtermGradeMustBeBetween0And100);
                break;

            case GradeType.Exam:
                if (value < 0 || value > 100)
                    throw new BussinessLogicException(GradeErrors.ExamGradeMustBeBetween0And100);
                break;
        }

        Value = value;
    }

    public static class GradeErrors
    {
        public static readonly Error StudentIdCantBeNull = new(
            "Grade.StudentIdCantBeNull",
            "Идентификатор студента обязателен."
        );

        public static readonly Error CourseSubjectIdCantBeNull = new(
            "Grade.CourseSubjectIdCantBeNull",
            "Идентификатор предмета в рамках курса обязателен."
        );

        public static readonly Error TypeIsInvalid = new(
            "Grade.TypeIsInvalid",
            "Указан недопустимый тип оценки."
        );

        public static readonly Error DailyGradeMustBeBetween0And10 = new(
            "Grade.DailyGradeMustBeBetween0And10",
            "Ежедневная оценка должна быть в диапазоне от 0 до 10."
        );

        public static readonly Error MidtermGradeMustBeBetween0And100 = new(
            "Grade.MidtermGradeMustBeBetween0And100",
            "Рубежная оценка должна быть в диапазоне от 0 до 100."
        );
         
        public static readonly Error ExamGradeMustBeBetween0And100 = new(
            "Grade.ExamGradeMustBeBetween0And100",
            "Экзаменационная оценка должна быть в диапазоне от 0 до 100."
        );
    }

    public enum GradeType 
    { 
        Daily = 1,      // Ежедневная оценка
        Midterm = 2,    // Рубежная оценка
        Exam = 3        // Экзаменационная оценка
    }
}