using Microsoft.EntityFrameworkCore;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Infrastructure.Persistence.Contextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Role?> GetByNameAsync(string name)
        {
            var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
            return role is null ? null : role;
        }
    }
}
