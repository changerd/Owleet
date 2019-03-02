using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owleet.Models
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public Question(Guid testId)
        {
            Id = Guid.NewGuid();
            TestId = testId;
            Answers = new HashSet<Answer>();
        }
    }
}
