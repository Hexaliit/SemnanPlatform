using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.UserService
{
    public interface IUserContext
    {
        public CurrentUser? GetCurrentUser();
    }
}
