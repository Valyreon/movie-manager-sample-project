using System;

namespace ServiceLayer.Exceptions
{
    public class PasswordTooWeakException : Exception
    {
        public PasswordTooWeakException()
        {
        }

        public PasswordTooWeakException(string message) : base(message)
        {
        }
    }
}
