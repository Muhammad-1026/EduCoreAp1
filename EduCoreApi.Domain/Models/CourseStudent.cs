using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class CourseStudent : Entity
{
    public CourseStudent(Guid courseId, Guid studentId, Guid createdBy) : base(createdBy)
    {
        SetCourseId(courseId);
        SetStudentId(studentId);
    }

    public Guid CourseId { get; private set; }
    public Course Course { get; private set; } = default!;

    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = default!;

    public void SetCourseId(Guid courseId)
    {
        if (courseId == Guid.Empty)
            throw new BussinessLogicException(CourseStudentErrors.CourseIdCantBeNull);

        CourseId = courseId;
    }

    public void SetStudentId(Guid studentId)
    {
        if (studentId == Guid.Empty)
            throw new BussinessLogicException(CourseStudentErrors.StudentIdCantBeNull);

        StudentId = studentId;
    }

    public static class CourseStudentErrors
    {
        public static readonly Error CourseIdCantBeNull = new(
            "CourseStudent.CourseIdCantBeNull",
            "Идентификатор курс не может быть пустым."
        );

        public static readonly Error StudentIdCantBeNull = new(
            "CourseStudent.StudentIdCantBeNull",
            "Идентификатор студент не может быть пустым."
        );
    }
}