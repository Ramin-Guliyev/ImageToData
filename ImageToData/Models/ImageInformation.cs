using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageToData.Models
{
    public class ImageInformation
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public ulong ImageHash { get; set; }
        public string ImageMachUrl { get; set; }
    }
}
