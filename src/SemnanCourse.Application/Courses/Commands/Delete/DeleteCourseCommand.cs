using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.Delete
{
    public class DeleteCourseCommand : IRequest
    {
        public DeleteCourseCommand(int id)
        {
            Id = id;
        }
        public int Id { get;}
    }
}
