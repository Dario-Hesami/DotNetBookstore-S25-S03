using DotNetBookstore.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBookstore.Controllers
{
    public class ShopController : Controller
    {
        // This controller is responsible for handling requests related to the shop
        // class level DbContext connection object
        // This is used to access the database
        private readonly ApplicationDbContext _context;

        // constructor that accepts a DbContext instance
        // This allows dependency injection to provide the context
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // query the list of categories from the database in a-z order for display
            var categories = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();
            return View(categories);
        }

        //  Get: /Shop/ShopByCategory/5
        public IActionResult ShopByCategory(int id)
        {
            // Display the category name on the page based on the ID - store category name in the ViewBag object
            var category = _context.Categories.Find(id);

            // return to /shop/index if category not found
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Category = category.Name;

            // query the books filtered by the selected CategoryId parameter
            var books = _context.Books.Where(b => b.CategoryId == id)
                .OrderBy(b => b.Title)
                .ToList();

            // send the books to the view for display   
            return View(books);
        }
    }
}
