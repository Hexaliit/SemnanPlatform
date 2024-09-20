using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHost;

        public ImageRepository(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        public async Task<string> CreateImage(IFormFile imageFile)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            
            fileName += Path.GetExtension(imageFile.FileName);

            var imageFolder = webHost.WebRootPath + "\\photos\\";

            using (var stream = System.IO.File.Create(imageFolder + fileName))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public void DeleteImage(string imageFIle)
        {
            var imageFolder = webHost.WebRootPath + "\\photos\\";
            System.IO.File.Delete(imageFolder + imageFIle);
        }
    }
}
