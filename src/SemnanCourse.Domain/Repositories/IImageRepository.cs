using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Repositories
{
    public interface IImageRepository
    {
        public Task<string> CreateImage(IFormFile imageFile);
        public void DeleteImage(string imageFIle);
    }
}
