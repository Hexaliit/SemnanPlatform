using AutoMapper;
using MediatR;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Interfaces.Services;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.Update
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IApplicationAuthorizationService authorizationService;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IApplicationAuthorizationService authorizationService)
        {
            this.courseRepository = courseRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }
        public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.CourseId)
                ?? throw new NotFoundException(CourseMessages.NotFound);

            if (!authorizationService.Authorize(course))
            {
                throw new ForbiddenException(UserMessages.UnAuthorized);
            }

            if(!await categoryRepository.ExistAny(c => c.Id == request.CategoryId))
            {
                throw new NotFoundException(CategoryMessages.NotFound);
            }
            if(await courseRepository.ExistsAnyAsync(c => c.Title == request.Title))
            {
                throw new AlreadyExistsException(CourseMessages.CourseExists);
            }
            mapper.Map(request, course);

            await courseRepository.UpdateAsync(course);

        }
    }
}
