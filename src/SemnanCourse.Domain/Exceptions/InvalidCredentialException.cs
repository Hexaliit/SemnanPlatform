﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Exceptions
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException(string? message) : base(message)
        {
        }
    }
}
