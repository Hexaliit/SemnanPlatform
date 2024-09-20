using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Messages
{
    public static class CourseMessages
    {
        public const string NotFound = "Course whit this id not found.";
        public const string CourseExists = "Course whit this title already exits.";
        public const string VideosDependents = "Course whit this id has video dependents.";
        public const string UserDependents = "Course whit this id has user dependents.";
        public const string UserHasCourse = "This course is alread in your courses list.";
    }
}
