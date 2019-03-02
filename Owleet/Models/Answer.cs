using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owleet.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsTrue { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public Answer()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }
    }
}
