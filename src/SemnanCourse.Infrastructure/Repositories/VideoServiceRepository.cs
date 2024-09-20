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
    public class VideoServiceRepository : IVideoServiceRepository
    {
        private readonly IWebHostEnvironment webHost;

        public VideoServiceRepository(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        public async Task<string> CreateVideo(IFormFile file)
        {
            var videoFile = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            videoFile += Path.GetExtension(file.FileName);

            var videoFolder = webHost.WebRootPath + "\\videos\\";

            using (var stream = System.IO.File.Create(videoFolder + videoFile))
            {
                await file.CopyToAsync(stream);
            }

            return videoFile;
        }

        public void DeleteVideo(string file)
        {
            var videoFolder = webHost.WebRootPath + "\\videos\\";

            System.IO.File.Delete(videoFolder + file);
        }
    }
}
