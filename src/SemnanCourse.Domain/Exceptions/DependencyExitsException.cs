using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Exceptions
{
    public class DependencyExitsException : Exception
    {
        public DependencyExitsException(string? message) : base(message)
        {
        }
    }
}
