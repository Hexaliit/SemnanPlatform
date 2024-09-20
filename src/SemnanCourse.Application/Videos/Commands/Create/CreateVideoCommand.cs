using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Commands.Create
{
    public class CreateVideoCommand : IRequest
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public bool ShowOnDemo { get; set; }
        public IFormFile Video { get; set; }
    }
}
