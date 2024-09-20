using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.UserService
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string Emial { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
