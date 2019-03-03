using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Owleet.Filters;
using Owleet.Models;
using Owleet.Models.DataRepository;

namespace Owleet.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITestDataRepository db;
        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
        public TestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = new TestDataRepository(context);
            _userManager = userManager;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await db.GetTestsByUserId(GetCurrentUserAsync().Result.Id));
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Rating,IsTimeLimit,IsPrivate,ErrorCount")] Test test)
        {
            
            test.UserId = GetCurrentUserAsync().Result.Id;
            await db.AddAsync(test);
            return RedirectToAction(nameof(Index));
        }

        // GET: Test/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Test>))]
        [ServiceFilter(typeof(ValidateTestAuthorAttribute))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return View(await db.GetTest(id.Value));
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Rating,IsTimeLimit,IsPrivate,ErrorCount,UserId")] Test test)
        {
            try
            {
                await db.UpdateAsync(test);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.ItemExists(x=>x.Id == test.Id).Result)
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Test/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Test>))]
        [ServiceFilter(typeof(ValidateTestAuthorAttribute))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return PartialView(await db.GetSingleAsync(x=>x.Id == id));
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await db.RemoveAsync(await db.GetSingleAsync(x => x.Id == id));
            return RedirectToAction(nameof(Index));
        }
    }
}
