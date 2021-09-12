﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
