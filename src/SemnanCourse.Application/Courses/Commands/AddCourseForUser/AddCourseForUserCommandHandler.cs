using MediatR;
using SemnanCourse.Application.Users.UserService;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.AddCourseForUser
{
    public class AddCourseForUserCommandHandler : IRequestHandler<AddCourseForUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        private readonly ICourseRepository courseRepository;

        public AddCourseForUserCommandHandler(IUserRepository userRepository,
            IUserContext userContext,
            ICourseRepository courseRepository)
        {
            this.userRepository = userRepository;
            this.userContext = userContext;
            this.courseRepository = courseRepository;
        }
        public async Task Handle(AddCourseForUserCommand request, CancellationToken cancellationToken)
        {
            var userId = userContext.GetCurrentUser()?.Id 
                ?? throw new NotFoundException(UserMessages.NotFound);

            var user = await userRepository.GetByIdAsync(int.Parse(userId), includeRoles: false, includeCourses: true)
                ?? throw new NotFoundException(UserMessages.NotFound);

            var course = await courseRepository.GetByIdAsync(request.CourseId)
                ?? throw new NotFoundException(CourseMessages.NotFound);

            foreach(var userCourse in user.Courses)
            {
                if(userCourse.Id == course.Id)
                {
                    throw new AlreadyExistsException(CourseMessages.UserHasCourse);
                }
            }

            if(course.Price > user.Balance)
            {
                throw new NotFoundException(UserMessages.InsuficentBalance);
            }

            user.Balance -= course.Price;

            user.Courses.Add(course);

            await userRepository.UpdateAsync(user);

        }
    }
}
