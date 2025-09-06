using EduCoreApi.Domain.Common;

namespace EduCoreApi.Domain.Models;

public class StudentDataFile : DataFile
{
    public StudentDataFile(string name, string url, long size, string extension) : base(name, url, size, extension)
    {
    }

    public Guid StudentId { get; set; }
}
