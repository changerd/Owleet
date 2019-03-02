namespace Owleet.Models.DataRepository
{
    public class AnswerDataRepository : GenericDataRepository<Answer>, IAnswerDataRepository
    {
        public AnswerDataRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
