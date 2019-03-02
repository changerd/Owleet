using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Owleet.Models;

namespace Owleet.Controllers
{
    public class TestController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly DataRepository db;
        public TestController(ApplicationDbContext context)
        {
            db = new DataRepository(context);
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await db.GetAllTests());
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        public IActionResult AddQuestion()
        {
            return PartialView("Create");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion([Bind("Id, Text")] Question question)
        {
            await db.AddQuestion(question);

            return PartialView("AddQuestion", question);
        }

        public IActionResult AddAnswer()
        {
            return PartialView("AddAnswer");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnswer([Bind("Id, Text")] Answer answer)
        {
            await db.AddAnswer(answer);

            return PartialView("AddAnswer", answer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Rating,IsTimeLimit,IsPrivate,ErrorCount,UserId")] Test test)
        {
            if (ModelState.IsValid)
            {
                await db.AddTest(test);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", test.UserId);
            return View(test);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            /*
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", test.UserId);
            return View(test);
            */
            return View();
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Rating,IsTimeLimit,IsPrivate,ErrorCount,UserId")] Test test)
        {
            if (id != test.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }*/
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", test.UserId);
            return View(test);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            /*
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
            */
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            /*
            var test = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */
            return null;
        }

        private void TestExists(Guid id)
        {
            //return _context.Tests.Any(e => e.Id == id);
        }
    }
}
