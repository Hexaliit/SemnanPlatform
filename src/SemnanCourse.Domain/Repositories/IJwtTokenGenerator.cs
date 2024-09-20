using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface IJwtTokenGenerator
    {
        public string Generate(User user);
    }
}
