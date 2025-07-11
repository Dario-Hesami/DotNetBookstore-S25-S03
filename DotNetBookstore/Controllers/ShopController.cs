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
    }
}
