using AutoMapper;
using MediatR;
using SemnanCourse.Application.Users.UserService;
using SemnanCourse.Application.Videos.Dto;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Queries.GetVideoById
{
    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, VideoDto>
    {
        private readonly IVideoRepository videoRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IUserContext userContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetVideoByIdQueryHandler(IVideoRepository videoRepository,
            ICourseRepository courseRepository,
            IUserContext userContext,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.videoRepository = videoRepository;
            this.courseRepository = courseRepository;
            this.userContext = userContext;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<VideoDto> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var video = await videoRepository.GetVideoByIdByCourseIdAsync(request.CourseId, request.VideoId, true)
                ?? throw new NotFoundException(VideoMessage.VideoNotFound);

            if(video.Course.Price > 0 && video.ShowOnDemo == false)
            {
                var userExists = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException();
                var user = await userRepository.GetByIdAsync(int.Parse(userExists.Id), includeRoles: false, includeCourses: true)
                    ?? throw new NotFoundException(UserMessages.NotFound);

                var courseExists = user.Courses.Contains(video.Course);
                if (!courseExists)
                {
                    throw new NotFoundException(UserMessages.PurchaseNeeded);
                }

            }

            var videoDto = mapper.Map<VideoDto>(video);
            return videoDto;
        }
    }
}
