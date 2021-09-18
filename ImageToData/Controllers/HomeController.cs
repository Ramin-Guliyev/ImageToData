using ImageToData.Data;
using ImageToData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageToData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AppDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(IndexModel model)
        {
            #region image
            string fullPath = null;
            string url = $"{_configuration["AppUrl"]}Images/default.jpg";
            string extension = Path.GetExtension(model.image.FileName);
            if (model.image.Length > 500000)
                return BadRequest();


            string newFileName = $"Images/{Guid.NewGuid()}{extension}";
            fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newFileName);
            url = $"{_configuration["AppUrl"]}{newFileName}";

            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await model.image.CopyToAsync(fs);
            }

            var hash = ImageHash.AverageHash(fullPath);
            #endregion


            #region data
            string fullPathData = null;
            string urlData = $"{_configuration["AppUrl"]}Images/default.jpg";
            string extensionData = Path.GetExtension(model.data.FileName);
            if (model.data.Length > 500000)
                return BadRequest();


            string newFileNameData = $"Images/{Guid.NewGuid()}{extensionData}";
            fullPathData = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newFileNameData);
            urlData = $"{_configuration["AppUrl"]}{newFileNameData}";

            using (var fs = new FileStream(fullPathData, FileMode.Create, FileAccess.Write))
            {
                await model.data.CopyToAsync(fs);
            }
            #endregion 


            await _context.Images.AddAsync(new ImageInformation
            {
                ImageHash = hash,
                ImageUrl = url,
                ImageName = newFileName,
                ImageMachUrl = urlData
            });

            _context.SaveChanges();

            return Ok(hash);
        }
        [HttpPost("/seach")]
        public async Task<IActionResult> Seach(IndexModel model)
        {
            string fullPath = null;
            string url = $"{_configuration["AppUrl"]}Images/default.jpg";
            string extension = Path.GetExtension(model.seachimage.FileName);
            if (model.seachimage.Length > 500000)
                return BadRequest();


            string newFileName = $"Images/{Guid.NewGuid()}{extension}";
            fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newFileName);
            url = $"{_configuration["AppUrl"]}{newFileName}";

            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await model.seachimage.CopyToAsync(fs);
            }

            var hash = ImageHash.AverageHash(fullPath);

            var hashList = await _context.Images.ToListAsync();

            foreach (var item in hashList)
            {
                var percentage = ImageHash.Similarity(hash, item.ImageHash);
                if (percentage >= 90)
                {
                    //  return Ok(item.ImageMachUrl);
                    return Redirect(item.ImageMachUrl);
                }
            }
            return BadRequest();
        }
    }
}
