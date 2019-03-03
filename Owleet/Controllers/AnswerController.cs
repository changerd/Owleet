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
    public class AnswerController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IAnswerDataRepository db;
        public AnswerController(ApplicationDbContext context)
        {
            db = new AnswerDataRepository(context);
        }

        // GET: Test/Create
        public IActionResult Create(Guid questionId)
        {
            var answer = new Answer(){QuestionId = questionId};
            return PartialView(answer);
        }
        
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,IsTrue,QuestionId")] Answer answer)
        {
            await db.AddAsync(answer);
            return PartialView(answer);
        }

        // GET: Test/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Answer>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return PartialView(await db.GetSingleAsync(x => x.Id == id));
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Text,IsTrue,QuestionId")] Answer answer)
        {
            try
            {
                await db.UpdateAsync(answer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.ItemExists(x=>x.Id == id).Result)
                {
                    return NotFound();
                }
                throw;
            }
            return PartialView(answer);
        }

        // GET: Test/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Answer>))]
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
