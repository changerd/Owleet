using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Owleet.Models.DataRepository
{
    public class TestDataRepository : GenericDataRepository<Test>, ITestDataRepository
    {
        public TestDataRepository(ApplicationDbContext context) : base(context)
        {
        }
        /*
        public async Task<List<Test>> GetAvailaibleTestsByUserId(Guid id)
        {
            return await _context.Tests.ToListAsync();
        }
        */
        public async Task<List<Test>> GetPassedTestsByUserId(Guid id)
        {
            var user = await context.Users.Include(x => x.UserAnswers).ThenInclude(x => x.Answer).ThenInclude(x => x.Question).ThenInclude(x => x.Test)
                .SingleOrDefaultAsync(x => x.Id == id.ToString());
            return user.Tests.ToList();
        }

        public async Task<List<Test>> GetAllTests()
        {
            return await context.Tests.ToListAsync();
        }

        public async Task<Test> GetTest(Guid id)
        {
            return await context.Tests.Include(x => x.Questions).ThenInclude(x => x.Answers)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
