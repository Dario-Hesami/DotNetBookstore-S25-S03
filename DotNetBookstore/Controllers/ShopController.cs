using DotNetBookstore.Data;
using DotNetBookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // POST: /Shop/AddToCart
        [HttpPost]
        public IActionResult AddToCart(int BookId, int Quantity)
        {
            // get book - to access the current book price
            var book = _context.Books.Find(BookId);

            // check if this cart already has this book 
            // if not, create a new CartItem
            var cartItem = _context.CartItems.SingleOrDefault(c => c.BookId == BookId &&
                c.CustomerId == GetCustomerId());

            if (cartItem == null)
            {
                // create a new CartItem
                cartItem = new CartItem
                {
                    BookId = BookId,
                    Quantity = Quantity,
                    Price = book.Price,
                    CustomerId = GetCustomerId()
                };

                _context.Add(cartItem);
            }
            // user already has this book in cart - update the quantity
            else
            {
                cartItem.Quantity += Quantity;
                _context.Update(cartItem);
            }

            _context.SaveChanges();

            // display the cart page
            return RedirectToAction("Cart");
        }

        // identify customer for each shopping cart
        private string GetCustomerId()
        {
            // is CustomerId session var already set?
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                // if user is logged in, use their email as session var
                if (User.Identity.IsAuthenticated)
                {
                    // use user's email address as CustomerId
                    HttpContext.Session.SetString("CustomerId", User.Identity.Name);
                }
                else
                {
                    // if user is not logged in, create a new session var using a GUID
                    HttpContext.Session.SetString("CustomerId", Guid.NewGuid().ToString());
                }
            }

            // return the session var so we can identify this user's cart
            return HttpContext.Session.GetString("CustomerId");
        }

        // GET: /Shop/Cart
        public IActionResult Cart()
        {
            // get current customer to filter CartItems query to fetch & display
            var customerId = GetCustomerId();

            // get current cart items and parent Book objects for current customer - JOIN to parent Book to get Book details
            var cartItems = _context.CartItems
                .Include(c => c.Book).OrderBy(c => c.Book.Title)
                .Where(c => c.CustomerId == customerId).ToList();

            // count total of items in cart - for navbar display & store in Session var
            var itemCount = (from c in cartItems
                             select c.Quantity).Sum();
            HttpContext.Session.SetInt32("ItemCount", itemCount);

            return View(cartItems);
        }

        // GET: /Shop/RemoveFromCart/6
        public IActionResult RemoveFromCart(int id)
        {
            // find the item for deletion
            var cartItem = _context.CartItems.Find(id);

            // delete
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            // refresh and display cart
            return RedirectToAction("Cart");
        }

    }
}