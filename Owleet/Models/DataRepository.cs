using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Owleet.Models
{
    public class DataRepository
    {
        private readonly ApplicationDbContext _context;
        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTest(Test test)
        {
            await _context.AddAsync(test);
            /*foreach (var question in test.Questions)
            {
                await _context.AddAsync(question);
                foreach (var answer in question.Answers)
                {
                    await _context.AddAsync(answer);
                }
            }*/
            await _context.SaveChangesAsync();
        }

        public async Task AddQuestion(Question question)
        {
            await _context.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task AddAnswer(Answer answer)
        {
            await _context.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        /*
        public async Task<List<Test>> GetAvailaibleTestsByUserId(Guid id)
        {
            return await _context.Tests.ToListAsync();
        }
        */
        public async Task<List<Test>> GetPassedTestsByUserId(Guid id)
        {
            var user = await _context.Users.Include(x => x.Answers).ThenInclude(x => x.Question).ThenInclude(x => x.Test)
                .SingleOrDefaultAsync(x => x.Id == id.ToString());
            return user.Tests.ToList();
        }

        public async Task<List<Test>> GetAllTests()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test> GetTest(Guid id)
        {
            return await _context.Tests.Include(x => x.Questions).ThenInclude(x => x.Answers)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
