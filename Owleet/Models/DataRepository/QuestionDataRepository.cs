using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Owleet.Models.DataRepository
{
    public class QuestionDataRepository : GenericDataRepository<Question>, IQuestionDataRepository
    {
        public QuestionDataRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task RemoveQuestionById(Guid id)
        {
            var question = await context.Questions.Include(x => x.Answers).SingleOrDefaultAsync(x => x.Id == id);
            foreach (Answer answer in question.Answers)
            {
                context.Entry(answer).State = EntityState.Deleted;
            }
            context.Entry(question).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<Test> GetTest(Guid testId)
        {
            return await context.Tests.Include(x => x.Questions).ThenInclude(x => x.Answers)
                .SingleOrDefaultAsync(x => x.Id == testId);
        }
    }
}
