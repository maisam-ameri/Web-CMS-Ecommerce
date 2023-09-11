using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Convertors
{
    public static class StringFixer
    {
        public static string FixEmail(this string email) => email.Trim().ToLower();
    }
}
