using MediatR;
using SemnanCourse.Application.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<CourseDto>
    {
        public GetCourseByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get;}
    }
}
