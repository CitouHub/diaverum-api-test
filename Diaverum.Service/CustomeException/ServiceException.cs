namespace Diaverum.Service.CustomeException
{
    public class ServiceException(
        ExceptionType exceptionType,
        Exception? innerException = null,
        string? details = null) : Exception(
            $"{(details != null ? details : "")}",
            innerException)
    {
        public readonly ExceptionType ExceptionType = exceptionType;
    }
}
