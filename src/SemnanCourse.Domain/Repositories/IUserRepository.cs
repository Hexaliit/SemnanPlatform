using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> ExitsAny(Expression<Func<User,bool>> predicate);
        public Task<User> CreateAsync(User user);
        public Task<User?> Authenticate(string email, string password);
        public Task<User?> GetByIdAsync(int id, bool includeRoles = false, bool includeCourses = false);
        public Task UpdateAsync(User user);
    }
}
