using MediatR;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Commands.UpdateVideoFile
{
    public class UpdateVideoFileCommandHandler : IRequestHandler<UpdateVideoFileCommand>
    {
        private readonly IVideoRepository videoRepository;
        private readonly IVideoServiceRepository videoServiceRepository;

        public UpdateVideoFileCommandHandler(IVideoRepository videoRepository,
            IVideoServiceRepository videoServiceRepository)
        {
            this.videoRepository = videoRepository;
            this.videoServiceRepository = videoServiceRepository;
        }
        public async Task Handle(UpdateVideoFileCommand request, CancellationToken cancellationToken)
        {
            var video = await videoRepository.GetVideoByIdByCourseIdAsync(request.CourseId, request.VideoId)
                ?? throw new NotFoundException(VideoMessage.VideoNotFound);

            var path = await videoServiceRepository.CreateVideo(request.Video);

            videoServiceRepository.DeleteVideo(video.Path);

            video.Path = path;

            await videoRepository.UpdateAsync(video);

        }
    }
}
