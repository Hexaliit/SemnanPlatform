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

namespace SemnanCourse.Application.Videos.Commands.Delete
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand>
    {
        private readonly IMapper mapper;
        private readonly IVideoRepository videoRepository;
        private readonly IVideoServiceRepository videoServiceRepository;
        private readonly IApplicationAuthorizationService authorizationService;

        public DeleteVideoCommandHandler(IMapper mapper,
            IVideoRepository videoRepository,
            IVideoServiceRepository videoServiceRepository,
            IApplicationAuthorizationService authorizationService)
        {
            this.mapper = mapper;
            this.videoRepository = videoRepository;
            this.videoServiceRepository = videoServiceRepository;
            this.authorizationService = authorizationService;
        }
        public async Task Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await videoRepository.GetVideoByIdByCourseIdAsync(request.CourseId, request.VideoId,includeCourse:true)
                ?? throw new NotFoundException(VideoMessage.VideoNotFound);

            if (!authorizationService.Authorize(video.Course))
            {
                throw new ForbiddenException(UserMessages.UnAuthorized);
            }

            videoServiceRepository.DeleteVideo(video.Path);

            await videoRepository.DeleteAsync(video);
        }
    }
}
