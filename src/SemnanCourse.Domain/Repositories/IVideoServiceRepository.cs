using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface IVideoServiceRepository
    {
        Task<string> CreateVideo(IFormFile file);
        void DeleteVideo(string file);
    }
}
