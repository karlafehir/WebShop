using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _context.Categories.ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(category.Name.Length < 4)
            {
                ModelState.AddModelError("Name", "Name must be longer than 4 characters");
            }

            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order ..");
            }

            if(ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Edit(int? categoryId)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId); //ovo mozemo umjesto category1 i 2
            //Category? category1 = _context.Categories.Find(categoryId);
            //Category? category2 = _context.Categories.Where(category => category.Id == categoryId).FirstOrDefault();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
           

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }


    }
}
