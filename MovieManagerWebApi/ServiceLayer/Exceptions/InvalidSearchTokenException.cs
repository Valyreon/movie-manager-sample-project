using System;

namespace ServiceLayer.Exceptions
{
    public class InvalidSearchTokenException : Exception
    {
        public InvalidSearchTokenException() : base()
        {
        }

        public InvalidSearchTokenException(string message) : base(message)
        {
        }

        public InvalidSearchTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
