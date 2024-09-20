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

namespace SemnanCourse.Application.Videos.Commands.Update
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IVideoRepository videoRepository;
        private readonly IMapper mapper;
        private readonly IApplicationAuthorizationService authorizationService;

        public UpdateVideoCommandHandler(ICourseRepository courseRepository,
            IVideoRepository videoRepository,
            IMapper mapper,
            IApplicationAuthorizationService authorizationService)
        {
            this.courseRepository = courseRepository;
            this.videoRepository = videoRepository;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }
        public async Task Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await videoRepository.GetVideoByIdByCourseIdAsync(request.CourseId, request.VideoId, includeCourse:true)
                ?? throw new NotFoundException(VideoMessage.VideoNotFound);

            if (!authorizationService.Authorize(video.Course))
            {
                throw new ForbiddenException(UserMessages.UnAuthorized);
            }

            mapper.Map(request, video);

            await videoRepository.UpdateAsync(video);
        }
    }
}
