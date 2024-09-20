using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
