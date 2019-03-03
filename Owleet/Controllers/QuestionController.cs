using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Owleet.Filters;
using Owleet.Models;
using Owleet.Models.DataRepository;

namespace Owleet.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IQuestionDataRepository db;
        public QuestionController(ApplicationDbContext context)
        {
            db = new QuestionDataRepository(context);
        }

        // GET: Test
        public async Task<IActionResult> Index(Guid testId)
        {
            return PartialView(await db.GetTest(testId));
        }

        // GET: Test/Create
        public IActionResult Create(Guid testId)
        {
            var question = new Question {TestId = testId};
            return PartialView(question);
        }
        
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,TestId")] Question question)
        {
            await db.AddAsync(question);
            return PartialView("Create", question);
        }

        // GET: Test/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Question>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return PartialView(await db.GetSingleAsync(x => x.Id == id));
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TestId,Text")] Question question)
        {
            try
            {
                await db.UpdateAsync(question);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.ItemExists(x=>x.Id == question.Id).Result)
                {
                    return NotFound();
                }
                throw;
            }
            return PartialView(question);
        }
        // GET: Test/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Question>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return PartialView(await db.GetSingleAsync(x => x.Id == id));
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await db.RemoveAsync(await db.GetSingleAsync(x => x.Id == id));
            return PartialView();
        }
    }
}
