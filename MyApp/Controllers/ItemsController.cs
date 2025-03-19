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

        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Create(string Name, double Price) {

            try
            {
             
                if (ModelState.IsValid && !(String.IsNullOrEmpty(Name) || Double.IsNaN(Price)))
                {
                    var item = new Item() { Name = Name, Price = Price };
                    _context.Items.Add(item);
                    _context.SaveChanges();
                    Console.WriteLine(item);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Index");
        }


        //public async Task<IActionResult> Edit(int id)
        //{
        //    var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

        //    return View(item);
        //}

        [HttpPost]
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

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if(item != null) ViewData["response_id"] = item.Id;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);

            try
            {
                if (item != null)
                {
                    _context.Items.Remove(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
               
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(item);
        }
    }
}
