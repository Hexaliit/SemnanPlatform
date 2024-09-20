using AutoMapper;
using MediatR;
using SemnanCourse.Application.Videos.Dto;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, IEnumerable<VideosForCourseDto>>
    {
        private readonly IMapper mapper;
        private readonly IVideoRepository videoRepository;

        public GetAllVideosQueryHandler(IMapper mapper,
            IVideoRepository videoRepository)
        {
            this.mapper = mapper;
            this.videoRepository = videoRepository;
        }
        public async Task<IEnumerable<VideosForCourseDto>> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
        {
            var videos = await videoRepository.GetAllVideosByCourseIdAsync(request.CourseId)
                ?? throw new NotFoundException(VideoMessage.CourseNotFound);

            var videosDto = new List<VideosForCourseDto>();
            foreach(var video in videos)
            {
                videosDto.Add(mapper.Map<VideosForCourseDto>(video));
            }
            return videosDto;
        }
    }
}
