using ImageToData.Data;
using ImageToData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ImageToData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        //private readonly AppDbContext _context;

        //public ImageController(IConfiguration configuration, AppDbContext context)
        //{
        //    _configuration = configuration;
        //    _context = context;
        //}

        //[HttpPost]
        //public async Task<IActionResult> ImageAdd(IFormFile file)
        //{
        //    string fullPath = null;
        //    string url = $"{_configuration["AppUrl"]}Images/default.jpg";
        //    string extension = Path.GetExtension(file.FileName);
        //    if (file.Length > 500000)
        //        return BadRequest();
               

        //    string newFileName = $"Images/{Guid.NewGuid()}{extension}";
        //    fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newFileName);
        //    url = $"{_configuration["AppUrl"]}{newFileName}";

        //    using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        //    {
        //        await file.CopyToAsync(fs);
        //    }

        //    var hash = ImageHash.AverageHash(fullPath);

        //    await _context.Images.AddAsync(new ImageInformation {
        //        ImageHash = hash,
        //        ImageName = newFileName,
        //        ImageMachUrl = url
        //    });

        //    _context.SaveChanges();

        //    return Ok(hash + "  " + url);
        //}
    }
}
