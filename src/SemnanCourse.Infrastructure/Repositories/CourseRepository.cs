using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SemnanCourse.Domain.Constants;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Models;
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
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAnyAsync(Expression<Func<Course, bool>> predicate)
        {
            return await dbContext.Courses.AnyAsync(predicate);
        }

        public async Task<List<Course>> GetAllAsync()
        {
            var courses = await dbContext.Courses.ToListAsync();
            return courses;
        }

        public async Task<(IEnumerable<Course>, int)> GetAllMatchingAsync(string? search, string? orderBy, int pageSize, int pageNumber, SortDirection? sortDirection = SortDirection.Accesnding)
        {
            var searchLower = search?.ToLower();
            var query = dbContext.Courses
                .Where(c => search == null || c.Title.Contains(search) || c.Description.Contains(search));

            var totalCount = await query.CountAsync();

            if (orderBy != null)
            {
                var allowedcolumn = new Dictionary<string, Expression<Func<Course, object>>>()
                {
                    {nameof(Course.Title), c => c.Title},
                    {nameof(Course.Price), c => c.Price},
                    {"Date", c => c.CreatedAt }
                };

                var selectedColumn = allowedcolumn[orderBy];

                query = sortDirection == SortDirection.Accesnding
                    ? query.OrderBy(selectedColumn)
                    : query.OrderByDescending(selectedColumn);
            }

            var courses = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (courses, totalCount);

        }

        public async Task<Course?> GetByIdAsync(int id, bool includeCategory = false, bool includeVideos = false,bool includeUsers = false)
        {
            var query = dbContext.Courses.Where(c => c.Id == id);
            if (includeCategory)
            {
                query = query.Include(c => c.Category);
            }
            if (includeVideos)
            {
                query = query.Include(c => c.Videos);
            }
            if (includeUsers)
            {
                query = query.Include(c => c.Users);
            }
            var course = await query.FirstOrDefaultAsync();
            return course is null ? null : course;
        }

        public async Task UpdateAsync(Course course)
        {
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync();
        }
    }
}
