using EduCoreApi.Shared.Models;

namespace EduCoreApi.Shared.Exeptions;

public class BussinessLogicException : Exception
{
    public BussinessLogicException(Error error) : base(error.Message) { }

    public BussinessLogicException(Error error, Exception exception) : base(error.Message, exception) { }
}