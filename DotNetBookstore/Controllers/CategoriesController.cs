using DotNetBookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBookstore.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            // Use the Category model to generate 10 categories in memory for display in the view
            var categories = new List<Category>();
            for (int i = 1; i <= 10; i++)
            {
                categories.Add(new Category { CategoryId = i, Name = "Category " + i });
            }

            return View(categories);
        }

        public IActionResult Browse(string category)
        {

            if (category == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Category = category;           
            return View();
        }
    }
}
