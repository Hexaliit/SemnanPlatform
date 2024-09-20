using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Commands.UpdateUserBalance
{
    public class UpdateUserBalanceCommand :IRequest
    {
        public int userId { get; set; }
        public decimal Amount { get; set; }
    }
}
