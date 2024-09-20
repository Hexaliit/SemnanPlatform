using AutoMapper;
using MediatR;
using SemnanCourse.Application.Courses.Dto;
using SemnanCourse.Domain.Models;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, PagedResult<CourseDto>>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public GetAllCoursesQueryHandler(ICourseRepository courseRepository,
            IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }
        public async Task<PagedResult<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var (courses,totalcount) = await courseRepository.GetAllMatchingAsync(request.Search, request.SortBy, request.PageSize, request.PageNumber, request.SortDirection);

            var count = courses.Count();
            
            var coursesDto = mapper.Map<IEnumerable<CourseDto>>(courses);

            foreach(var courseDto in coursesDto)
            {
                courseDto.Category = null;
            }

            var result = new PagedResult<CourseDto>(coursesDto, totalcount, request.PageSize, request.PageNumber);
            return result;

            

            
        }
    }
}
