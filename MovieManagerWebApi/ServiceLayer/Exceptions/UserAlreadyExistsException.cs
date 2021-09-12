using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
        {
        }

        public UserAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
