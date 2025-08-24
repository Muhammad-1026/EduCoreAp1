using EduCoreApi.Domain.Common;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

public class CourseTeacher : Entity
{
    public Guid CourseId { get; private set; }
    public Course Course { get; private set; } = default!;

    public Guid TeacherId { get; private set; }
    public Teacher Teacher { get; private set; } = default!;

    public bool IsCurator { get; private set; }

    public CourseTeacher(Guid courseId, Guid teacherId, bool isCurator, Guid createdBy)
        : base(createdBy)
    {
        SetCourseId(courseId);
        SetTeacherId(teacherId);
        SetIsCurator(isCurator);
    }

    public void SetCourseId(Guid courseId)
    {
        if (courseId == Guid.Empty)
            throw new BussinessLogicException(CourseTeacherErrors.CourseIdCantBeNull);

        CourseId = courseId;
    }

    public void SetTeacherId(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new BussinessLogicException(CourseTeacherErrors.TeacherIdCantBeNull);

        TeacherId = teacherId;
    }

    public void SetIsCurator(bool isCurator)
    {
        if (isCurator && Course != null && Course.Teachers.Any(t => t.IsCurator && t.TeacherId != TeacherId))
            throw new BussinessLogicException(CourseTeacherErrors.CuratorAlreadyExists);

        IsCurator = isCurator;
    }

    public static class CourseTeacherErrors
    {
        public static readonly Error CourseIdCantBeNull = new(
            "CourseTeacher.CourseIdCantBeNull",
            "Идентификатор курса не может быть пустым."
        );

        public static readonly Error TeacherIdCantBeNull = new(
            "CourseTeacher.TeacherIdCantBeNull",
            "Идентификатор преподавателя не может быть пустым."
        );

        public static readonly Error CuratorAlreadyExists = new(
            "CourseTeacher.CuratorAlreadyExists",
            "У этого курса уже назначен куратор."
        );
    }
}
