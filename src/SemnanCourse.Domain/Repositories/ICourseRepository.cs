using SemnanCourse.Domain.Constants;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface ICourseRepository
    {
        public Task<bool> ExistsAnyAsync(Expression<Func<Course, bool>> predicate);
        public Task<List<Course>> GetAllAsync();
        public Task CreateAsync(Course course);
        public Task<Course?> GetByIdAsync(int id, bool includeCategory = false, bool includeVideos = false, bool includeUsers = false);
        public Task UpdateAsync(Course course);
        public Task DeleteAsync(Course course);
        public Task<(IEnumerable<Course>, int)> GetAllMatchingAsync(string? search, string? orderBy, int pageSize, int pageNumber, SortDirection? sortDirection = SortDirection.Accesnding);
    }
}
