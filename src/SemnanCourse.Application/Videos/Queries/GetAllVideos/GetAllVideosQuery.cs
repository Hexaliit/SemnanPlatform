using MediatR;
using SemnanCourse.Application.Videos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQuery : IRequest<IEnumerable<VideosForCourseDto>>
    {
        public GetAllVideosQuery(int courseId)
        {
            CourseId = courseId;
        }
        public int CourseId { get;}
    }
}
