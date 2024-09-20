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
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext dbContext;

        public VideoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Video?>> GetAllVideosByCourseIdAsync(int courseId, bool includeCourse = false)
        {
            var query = dbContext.Videos.Where(v => v.CourseId == courseId);
            if (includeCourse)
            {
                query = query.Include(v => v.Course);
            }
            var courses = await query.ToListAsync();
            return courses;
        }

        public async Task<bool> ExistsAny(Expression<Func<Video, bool>> predicate)
        {
            return await dbContext.Videos.AnyAsync(predicate);
        }

        public async Task CreateAsync(Video video)
        {
            await dbContext.Videos.AddAsync(video);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Video?> GetVideoByIdByCourseIdAsync(int courseId, int videoId, bool includeCourse = false)
        {
            var query = dbContext.Videos.Where(v => v.CourseId == courseId && v.Id == videoId);

            if (includeCourse)
            {
                query = query.Include(v => v.Course);
            };

           var video = await query.FirstOrDefaultAsync();

           return video;
        }

        public async Task UpdateAsync(Video video)
        {
            dbContext.Videos.Update(video);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Video video)
        {
            dbContext.Videos.Remove(video);
            await dbContext.SaveChangesAsync();
        }
    }
}
