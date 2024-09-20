using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync();
        public Task<Category?> GetByIdAsync(int id, bool includeChildren = false);
        public Task<bool> ExistAny(Expression<Func<Category, bool>> predicate);
        public Task<Category> CreateAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task DeleteAsync(Category category);

    }
}
