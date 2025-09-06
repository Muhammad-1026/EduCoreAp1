using EduCoreApi.Domain.Common;

namespace EduCoreApi.Domain.Models;

public class TeacherDataFile : DataFile
{
    public TeacherDataFile(string name, string url, long size, string extension) : base(name, url, size, extension)
    {
    }

    public Guid TeacherId { get; set; }
}
