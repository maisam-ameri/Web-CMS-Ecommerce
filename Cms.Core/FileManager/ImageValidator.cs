using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.FileManager
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                if (file == null) return false;

                    var image = System.Drawing.Image.FromStream(file.OpenReadStream());

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
