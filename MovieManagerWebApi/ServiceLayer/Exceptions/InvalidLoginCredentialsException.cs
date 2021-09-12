using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Exceptions
{
    public class InvalidLoginCredentialsException : Exception
    {
        public InvalidLoginCredentialsException()
        {
        }

        public InvalidLoginCredentialsException(string message) : base(message)
        {
        }
    }
}
