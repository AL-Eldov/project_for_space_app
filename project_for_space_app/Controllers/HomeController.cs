using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_for_space_app.Models;

namespace project_for_space_app.Controllers;

public class HomeController : Controller
{
    ApplicationContext db;
    public HomeController(ApplicationContext context)
    {
        db = context;
    }
    public async Task<IActionResult> ShowUsers()
    {
        return View(await db.Users.ToListAsync());
    }
    public IActionResult CreateUser()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return RedirectToAction("ShowUsers");
    }
    public async Task<IActionResult> DeleteUser(int? id)
    {
        if (id != null)
        {
            User user = new User { Id = id.Value };
            db.Entry(user).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return RedirectToAction("ShowUsers");
        }
        return NotFound();
    }
    public async Task<IActionResult> EditUser(int? id)
    {
        if (id != null)
        {
            User? user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user != null)
                return View(user);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> EditUser(User user)
    {
        db.Users.Update(user);
        await db.SaveChangesAsync();
        return RedirectToAction("ShowUsers");
    }
    public async Task<IActionResult> ShowPurchases()
    {
        return View(await db.Purchases.Include(u => u.user).ToListAsync());
    }
    public IActionResult CreatePurchase()
    {
        ViewBag.userIds = db.Users.Select(u => u.Id).ToList();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreatePurchase(Purchase purchase)
    {
        purchase.user = db.Users.FirstOrDefault(p => p.Id == purchase.Id);
        db.Purchases.Add(purchase);
        await db.SaveChangesAsync();
        return RedirectToAction("ShowPurchases");
    }
    public async Task<IActionResult> DeletePurchase(int? id)
    {
        if (id != null)
        {
            Purchase purchase = new Purchase { Id = id.Value };
            db.Entry(purchase).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return RedirectToAction("ShowPurchases");
        }
        return NotFound();
    }
    public async Task<IActionResult> EditPurchase(int? id)
    {
        if (id != null)
        {
            Purchase? purchase = await db.Purchases.FirstOrDefaultAsync(p => p.Id == id);
            if (purchase != null)
            {
                ViewBag.userIds = db.Users.Select(u => u.Id).ToList();
                return View(purchase);
            }
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> EditPurchase(Purchase purchase)
    {
        purchase.user = db.Users.FirstOrDefault(p => p.Id == purchase.userId);
        db.Purchases.Update(purchase);
        await db.SaveChangesAsync();
        return RedirectToAction("ShowPurchases");
    }
}
