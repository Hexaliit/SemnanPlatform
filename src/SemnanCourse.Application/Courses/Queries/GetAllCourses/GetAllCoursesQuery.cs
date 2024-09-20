using MediatR;
using SemnanCourse.Application.Courses.Dto;
using SemnanCourse.Domain.Constants;
using SemnanCourse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQuery : IRequest<PagedResult<CourseDto>>
    {
        public string? Search { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
