using MediatR;
using SemnanCourse.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserDto>
    {
    }
}
