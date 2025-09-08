namespace EduCoreApi.Domain.Common;

public abstract class DataFile(string name, string url, long size, string extension)
{
    public Guid Id { get; set; }
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
    public long Size { get; set; } = size;
    public string Extension { get; set; } = extension;
}
