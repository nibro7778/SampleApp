namespace SampleApp.Application.Common.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public IEnumerable<string> Error { get; }

        public ExternalServiceException(IEnumerable<string> error)
        {
            Error = error;
        }
    }
}
