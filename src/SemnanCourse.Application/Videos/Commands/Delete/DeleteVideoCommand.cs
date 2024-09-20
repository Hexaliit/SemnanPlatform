using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Commands.Delete
{
    public class DeleteVideoCommand : IRequest
    {
        public int CourseId { get; set; }
        public int VideoId { get; set; }
    }
}
