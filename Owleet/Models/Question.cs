using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owleet.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public Question()
        {
            Id = Guid.NewGuid();
            Answers = new HashSet<Answer>();
        }
    }
}
