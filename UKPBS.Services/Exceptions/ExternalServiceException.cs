using System;

namespace UKPBS.Services.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}