using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Generators
{
    public class NameGenerator
    {
        public static string  GenerateName() => Guid.NewGuid().ToString();
    }
}
