using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace Cms.Core.FileManager
{
    public class ImageManager
    {
        private IWebHostEnvironment _webHostEnvi;

        public ImageManager(IWebHostEnvironment webHostEnvironment)
        {
                _webHostEnvi = webHostEnvironment;
        }

        public string UploadImage(string image, IFormFile file, string imagePath)
        {
            var imageName = $"{Guid.NewGuid()}{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";
            var rootPath = _webHostEnvi.WebRootPath;
            var oldPath = Path.Combine(rootPath, imagePath, image);
            var path = Path.Combine(rootPath, imagePath, imageName.ToString());
            RemoveLastAvatar(oldPath);


            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Close();
            }

            return imageName;
        }



        private void RemoveLastAvatar(string path)
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

        }

        public void GenerateThumbnail(string fileName, string resourcePath, string savePath,int width)
        {

            var rootPath = _webHostEnvi.WebRootPath;
            var path = Path.Combine(rootPath, resourcePath, fileName);

            var fileInfo = new FileInfo(path);
            using (MagickImage magicImage = new MagickImage(fileInfo))
            {
                magicImage.Thumbnail(width, width);
                path = Path.Combine(rootPath, savePath, fileName);

                magicImage.Write(path);
            }




        }
    }
}
