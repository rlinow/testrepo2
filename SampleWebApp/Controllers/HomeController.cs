using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageSharp;


namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Test()
        {
            string onePixelGifBase64 = "R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";
            byte[] content = Convert.FromBase64String(onePixelGifBase64);
            Configuration.Default.AddImageFormat(new ImageSharp.Formats.GifFormat());
            var image = Image.Load(content);
            var ms = new System.IO.MemoryStream();
            image.Resize(new ImageSharp.Processing.ResizeOptions { Size = new Size(100, 100) });
            image.Save(ms);
            var newContent = ms.ToArray();
            return File(newContent, "image/gif");
        }
    }
}
