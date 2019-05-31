using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EfCore30_IdsInValueObjects.Models;

namespace EfCore30_IdsInValueObjects.Controllers
{
    public class HomeController : Controller
    {
        public BlogDbContext DbContext { get; }

        public HomeController(BlogDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            var blog456 = DbContext.Blogs.Where(p => p.Id == new BlogId("456")).ToList();

            var postsOfBlog123 = DbContext.Posts.Where(p => p.BlogId == new BlogId("123")).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
