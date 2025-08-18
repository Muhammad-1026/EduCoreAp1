using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Grade : Entity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = default!;

    public Guid SubjectId { get; private set; }
    public  Subject Subject { get; private set; } = default!;

    public int Value { get; private set; }

    public Grade(Guid studentId, Guid subjectId, int value)
    {
        StudentId = studentId;
        SubjectId = subjectId;
        Value = value;
    }

    public void SetStudentId(Guid studentId)
    {
        if (studentId == Guid.Empty)
            throw new BussinessLogicException(GradeErrors.StudentIdCantBeNull);

        StudentId = studentId;
    }

    public void SetSubjectId(Guid subjectId)
    {
        if (subjectId == Guid.Empty)
            throw new BussinessLogicException(GradeErrors.SubjectIdCantBeNull);

        SubjectId = subjectId;
    }

    public void SetValue(int value)
    {
        if (value < 0 || value > 100)
            throw new BussinessLogicException(GradeErrors.ValueMustBeBetween0And100);

        Value = value;
    }

    public class GradeErrors
    {
        public static readonly Error StudentIdCantBeNull = new(
            "Grade.StudentIdCanBeNull",
            "Student ID cannot be null."
        );

        public static readonly Error SubjectIdCantBeNull = new(
            "Grade.SubjectIdCanBeNull",
            "Subject ID cannot be null."
        );

        public static readonly Error ValueMustBeBetween0And100 = new(
            "Grade.ValueMustBeBetween0And100",
            "Grade value must be between 0 and 100."
        );
    }
}

