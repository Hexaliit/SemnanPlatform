using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Login
{
    public class LoginResponse
    {
        public LoginResponse(string token)
        {
            JwtToken = token;
        }
        public string JwtToken { get; set; }
    }
}
