using _3._20_ImageLikes.Data;
using _3._20_ImageLikes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Diagnostics;
using System.Reflection;

namespace _3._20_ImageLikes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var repo = new ImageRepo(_connectionString);
            return View(new IndexViewModel
            {
                Images = repo.GetImages()
            });
        }


        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile imageFile, string title)
        {
            var fileName = $"{Guid.NewGuid()}-{imageFile.FileName}";
            var fullImgPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

            using FileStream fs = new FileStream(fullImgPath, FileMode.Create);
            imageFile.CopyTo(fs);

            var repo = new ImageRepo(_connectionString);
            repo.AddImage(new Image
            {
                Date = DateTime.Now,
                ImagePath = fileName,
                Title = title
            });
            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new ImageRepo(_connectionString);
            return View(new ViewImageViewModel
            {
                Image = repo.GetImageById(id)
            });
        }

        public void IncrementLikes(int id)
        {
            var imageIds = HttpContext.Session.Get<List<int>>("imageIds") ?? new List<int>();
            
            imageIds.Add(id);
            HttpContext.Session.Set("imageIds", imageIds);

            var repo = new ImageRepo(_connectionString);
            repo.IncrementLikes(id);
           
        }

        public IActionResult IsImageLiked(int id)
        {
            var imageIds = HttpContext.Session.Get<List<int>>("imageIds");
            var isLiked = imageIds.Contains(id) && imageIds != null;
            return Json(isLiked);
        }

        public IActionResult GetLikesByImageId(int id)
        {
            var repo = new ImageRepo(_connectionString);
            var likes = repo.GetLikesByImageId(id);
            return Json(likes);
        }

    }
}
