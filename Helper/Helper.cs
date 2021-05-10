using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.Helper.TagHelpers
{
    public class Helper: TagHelper
    {
        public string GetString()
        {
            return "abc";
        }
    }
}
