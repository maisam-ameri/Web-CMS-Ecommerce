using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Generators
{
    public class TagGenerator
    {
        public static string[] GenerateTags(string tag)
        {
            return tag.Split('،');
        }
    }
}
