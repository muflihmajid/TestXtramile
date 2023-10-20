using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SceletonAPI.Application.Interfaces;
using SceletonAPI.Application.Misc;

namespace SceletonAPI.Infrastructure.FileManager
{
    public class ManagedDiskUploader : IUploader
    {

        private IWebHostEnvironment _env;
        public ManagedDiskUploader(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFile(string source, string prefix, string filename)
        {
            var location = Utils.GetUploadLocation(prefix, filename);
            var path = _env.ContentRootPath + location;
            Byte[] bytes = Convert.FromBase64String(source);
            await File.WriteAllBytesAsync(path, bytes);
            return location;
        }
    }
}
