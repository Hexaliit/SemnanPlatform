using Microsoft.EntityFrameworkCore;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Infrastructure.Persistence.Contextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            dbContext.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAny(Expression<Func<Category, bool>> predicate)
        {
            return await dbContext.Categories.AnyAsync(predicate);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await dbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category?> GetByIdAsync(int id, bool includeChildren = false)
        {
            var query = dbContext.Categories.Where(c => c.Id == id).AsQueryable();

            if (includeChildren)
            {
                query = query.Include(c => c.Children);
            }


            var category = await query.FirstOrDefaultAsync();
            return category is null ? null : category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();
            return category;

        }
    }
}
