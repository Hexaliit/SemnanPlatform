using AutoMapper;
using SemnanCourse.Application.Videos.Commands.Create;
using SemnanCourse.Application.Videos.Commands.Update;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Dto
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoDto>();

            CreateMap<CreateVideoCommand, Video>()
                .ForMember(des => des.Path, opt => opt.Ignore());

            CreateMap<UpdateVideoCommand, Video>();

            CreateMap<Video, VideosForCourseDto>();
        }
    }
}
