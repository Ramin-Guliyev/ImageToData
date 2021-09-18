using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageToData.Models
{
    public class IndexModel
    {
        public IFormFile image { get; set; }
        public IFormFile data { get; set; }
        public IFormFile seachimage { get; set; }
    }
}
