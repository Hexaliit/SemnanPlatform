using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Interfaces.Services;
using SemnanCourse.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.UserService
{
    public class ApplicationAuthorizationService : IApplicationAuthorizationService
    {
        private readonly IUserContext userContext;

        public ApplicationAuthorizationService(IUserContext userContext)
        {
            this.userContext = userContext;
        }
        public bool Authorize(Course course)
        {
            var user = userContext.GetCurrentUser()
                ?? throw new NotFoundException(UserMessages.NotFound);

            if (!user.Roles.Contains("Admin"))
            {
                if(course.UserId != int.Parse(user.Id))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
