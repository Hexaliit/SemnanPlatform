using AutoMapper;
using SemnanCourse.Application.Users.Register;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Dto
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommand, User>();

            CreateMap<User,UserDto>();
        }
    }
}
