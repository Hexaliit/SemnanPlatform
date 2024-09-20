using MediatR;
using SemnanCourse.Application.Videos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Queries.GetVideoById
{
    public class GetVideoByIdQuery : IRequest<VideoDto>
    {
        public int CourseId { get; set; }
        public int VideoId { get; set; }
    }
}
