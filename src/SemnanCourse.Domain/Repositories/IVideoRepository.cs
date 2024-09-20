using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface IVideoRepository
    {
        public Task<IEnumerable<Video?>> GetAllVideosByCourseIdAsync(int courseId, bool includeCourse = false);
        public Task<bool> ExistsAny(Expression<Func<Video, bool>> predicate);
        public Task CreateAsync(Video video);
        public Task<Video?> GetVideoByIdByCourseIdAsync(int courseId, int videoId, bool includeCourse = false);
        public Task UpdateAsync(Video video);
        public Task DeleteAsync(Video video);
    }
}
