using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Infrastructure.Persistence.Contextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;

        public UserRepository(ApplicationDbContext dbContext,
            IPasswordHasher<User> passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null) return null;

            var verifiedHashedPassword = passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return verifiedHashedPassword == PasswordVerificationResult.Success
                ? user
                : null;

        }

        public async Task<User> CreateAsync(User user)
        {
            user.Password = passwordHasher.HashPassword(user, user.Password);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ExitsAny(Expression<Func<User, bool>> predicate)
        {
            return await dbContext.Users.AnyAsync(predicate);
        }

        public async Task<User?> GetByIdAsync(int id, bool includeRoles = false, bool includeCourses = false)
        {
            var query = dbContext.Users.Where(u => u.Id == id);
            if (includeRoles)
            {
                query = query.Include(u => u.Roles);
            }
            if (includeCourses)
            {
                query = query.Include(u => u.Courses);
            }

            var user = await query.FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            dbContext.Update(user);           
            await dbContext.SaveChangesAsync();
        }
    }
}
