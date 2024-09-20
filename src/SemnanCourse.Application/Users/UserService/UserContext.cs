using Microsoft.AspNetCore.Http;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.UserService
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor httpContext;

        public UserContext(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContext.HttpContext?.User;
            if (user == null)
            {
                throw new NotFoundException(UserMessages.UnAuthorized);
            }

            if(user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(c => c.Type == "Id")!.Value;
            var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return new CurrentUser()
            {
                Id = userId,
                Emial = userEmail,
                Roles = userRoles
            };
        }
    }
}
