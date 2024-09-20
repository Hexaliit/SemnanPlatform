using AutoMapper;
using MediatR;
using SemnanCourse.Application.Users.Dto;
using SemnanCourse.Application.Users.UserService;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserDto>
    {
        private readonly IUserContext userContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserProfileQueryHandler(IUserContext userContext,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.userContext = userContext;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userContextExists = userContext.GetCurrentUser()
                ?? throw new NotFoundException(UserMessages.NotFound);

            var user = await userRepository.GetByIdAsync(int.Parse(userContextExists.Id), includeRoles: false, includeCourses: true)
                ?? throw new NotFoundException(UserMessages.NotFound);

            var userDto = mapper.Map<UserDto>(user);

            foreach(var course in userDto.Courses)
            {
                course.Category = null;
            }

            return userDto;
        }
    }
}
