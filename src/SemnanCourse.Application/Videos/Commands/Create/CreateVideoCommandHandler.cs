using AutoMapper;
using MediatR;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Interfaces.Services;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Commands.Create
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IVideoRepository videoRepository;
        private readonly IVideoServiceRepository videoServiceRepository;
        private readonly IMapper mapper;
        private readonly IApplicationAuthorizationService authorizationService;

        public CreateVideoCommandHandler(ICourseRepository courseRepository,
            IVideoRepository videoRepository,
            IVideoServiceRepository videoServiceRepository,
            IMapper mapper,
            IApplicationAuthorizationService authorizationService)
        {
            this.courseRepository = courseRepository;
            this.videoRepository = videoRepository;
            this.videoServiceRepository = videoServiceRepository;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }
        public async Task Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.CourseId)
                ?? throw new NotFoundException(CourseMessages.NotFound);
            if (!authorizationService.Authorize(course))
            {
                throw new ForbiddenException(UserMessages.UnAuthorized);
            }
            var path = await videoServiceRepository.CreateVideo(request.Video);
            var video = mapper.Map<Video>(request);
            video.Path = path;
            video.Course = null;

            await videoRepository.CreateAsync(video);
        }
    }
}
