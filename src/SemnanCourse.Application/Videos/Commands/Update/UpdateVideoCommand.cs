using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Commands.Update
{
    public class UpdateVideoCommand : IRequest
    {
        public int VideoId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public bool ShowOnDemo { get; set; }
    }
}
