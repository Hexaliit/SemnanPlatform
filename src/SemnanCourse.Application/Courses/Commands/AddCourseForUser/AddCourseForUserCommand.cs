using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.AddCourseForUser
{
    public class AddCourseForUserCommand : IRequest
    {
        public int CourseId { get; set; }
    }
}
