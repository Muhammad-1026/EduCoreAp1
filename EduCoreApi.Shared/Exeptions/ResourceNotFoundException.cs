using EduCoreApi.Shared.Models;

namespace EduCoreApi.Shared.Exeptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(Error error) : base(error.Message) { }

    public ResourceNotFoundException(Error error, Exception exception) : base(error.Message, exception) { }
}