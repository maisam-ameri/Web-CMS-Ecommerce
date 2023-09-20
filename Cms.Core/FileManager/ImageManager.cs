using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.FileManager
{
    public class ImageManager
    {
        public string UploadAvatar(string oldname, IFormFile file)
        {
            var avatarName = $"{Guid.NewGuid()}{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";
            var rootPath = Directory.GetCurrentDirectory();
            var oldPath = Path.Combine(rootPath, "images/user/avatar/", oldname);
            var path = Path.Combine(rootPath, "images/user/avatar/", avatarName.ToString());
            RemoveLastAvatar(oldPath);


            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Close();
            }

            return avatarName;
        }

        private void RemoveLastAvatar(string path)
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

        }
    }
}
