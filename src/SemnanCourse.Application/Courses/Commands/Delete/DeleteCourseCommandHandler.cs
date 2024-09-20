using MediatR;
using Microsoft.AspNetCore.Mvc;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Interfaces.Services;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.Delete
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IImageRepository imageRepository;
        private readonly IApplicationAuthorizationService authorizationService;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository,
            IImageRepository imageRepository,
            IApplicationAuthorizationService authorizationService)
        {
            this.courseRepository = courseRepository;
            this.imageRepository = imageRepository;
            this.authorizationService = authorizationService;
        }
        public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.Id,includeCategory : false,includeVideos:true,includeUsers:true)
                ?? throw new NotFoundException(CourseMessages.NotFound);

            if (!authorizationService.Authorize(course))
            {
                throw new ForbiddenException(UserMessages.UnAuthorized);
            }

            if(course.Videos.Count > 0)
            {
                throw new DependencyExitsException(CourseMessages.VideosDependents);
            }
            if (course.Users.Count > 0)
            {
                throw new DependencyExitsException(CourseMessages.UserDependents);
            }

            imageRepository.DeleteImage(course.Avatar);

            await courseRepository.DeleteAsync(course);
        }
    }
}
