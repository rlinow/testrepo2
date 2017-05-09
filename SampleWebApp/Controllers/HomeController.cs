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

        public async Task<IActionResult> Test(int width = 500, int height = 500)
        {
            string url = "https://dwsimg.dealercenter.net/inv-img/SUFQd01nSWdGejd3a0JxR2FqbmhnWGErMFFHWU9VVnVvVWk1NVFnZCtYY1JiU3FldTZvblVyY2xyVS9Ja08vTlU0M2ZnK1lFWVh6ejJydHZNblBUcmc9PQ%3d%3d/67601140-0604-48e8-a6af-2d2bd970d352/640/480";
            byte[] content, resizedContent;
            using (var httpClient = new System.Net.Http.HttpClient()) {
                content = await (await httpClient.GetAsync(url)).Content.ReadAsByteArrayAsync();
            }
            Configuration.Default.AddImageFormat(new ImageSharp.Formats.GifFormat());
            var image = Image.Load(content, new ImageSharp.Formats.JpegDecoder());
            using (var ms = new System.IO.MemoryStream())
            {
                image.Resize(new ImageSharp.Processing.ResizeOptions { Size = new Size(width, height), Mode = ImageSharp.Processing.ResizeMode.Max });
                image.Save(ms, new ImageSharp.Formats.JpegEncoder());
                resizedContent = ms.ToArray();
            }
            return File(resizedContent, "image/jpeg");
        }
    }
}
