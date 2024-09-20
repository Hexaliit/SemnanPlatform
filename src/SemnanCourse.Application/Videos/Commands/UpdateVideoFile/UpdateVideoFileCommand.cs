using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Commands.UpdateVideoFile
{
    public class UpdateVideoFileCommand : IRequest
    {
        public int VideoId { get; set; }
        public int CourseId { get; set; }
        public IFormFile Video { get; set; }
    }
}
