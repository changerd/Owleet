using System;
using System.Threading.Tasks;

namespace Owleet.Models.DataRepository
{
    public interface IQuestionDataRepository : IGenericDataRepository<Question>
    {
        Task RemoveQuestionById(Guid id);
    }
}