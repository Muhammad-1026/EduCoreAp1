namespace EduCoreApi.Application.Common.Results;

public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = default!;
    public T? Data { get; set; }
}