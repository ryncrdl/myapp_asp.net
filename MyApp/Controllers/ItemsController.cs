using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        //Initalize DB Context
        protected readonly MyAppContext _context;

        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Price")] Item item) {

            try
            {
                if (ModelState.IsValid)
                {
                     _context.Items.Add(item);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return View(item);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            Console.WriteLine(item);
            return View(item);
        }

        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Items.Update(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(item);
        }

        //public IActionResult Overview()
        //{
        //    var item = new Item() { Name = "Keyboard" };
        //    return View(item);
        //}

        //public IActionResult Edit(int id) {
        //    return Content($"ID = {id}");
        //}
    }
}
