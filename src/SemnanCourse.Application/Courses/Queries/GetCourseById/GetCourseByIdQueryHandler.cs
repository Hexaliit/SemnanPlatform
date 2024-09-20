using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SemnanCourse.Application.Courses.Dto;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ILogger<GetCourseByIdQueryHandler> logger;
        private readonly IMapper mapper;

        public GetCourseByIdQueryHandler(ICourseRepository courseRepository,
            ILogger<GetCourseByIdQueryHandler> logger,
            IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Geting Course whit id {CourseId}", request.Id);
            var course = await courseRepository.GetByIdAsync(request.Id, includeCategory: true, includeVideos: true)
                ?? throw new NotFoundException(CourseMessages.NotFound);

            var courseDto = mapper.Map<CourseDto>(course);
            return courseDto;
        }
    }
}
